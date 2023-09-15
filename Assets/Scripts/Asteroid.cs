using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int asteroidLevel;
    public float asteroidHealth;
    public GameObject[] asteroids;
    public Transform playerPos;
    public float bulletDamage = 0.5f;

    public playerHealth playerHealthScript;
    private PolygonCollider2D asteroidCol;
    private Rigidbody2D rgbd; 
    private Transform asteroidTransf;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        asteroidHealth = asteroidLevel;
        asteroidTransf = this.transform;
        Vector3 asteroidSize = new Vector2(10, 10) * asteroidLevel;
        asteroidTransf.localScale = asteroidSize;
        rgbd = this.GetComponent<Rigidbody2D>();
        asteroidCol = this.GetComponent<PolygonCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        GameObject player = GameObject.Find("Player");
        playerHealthScript = player.GetComponent<playerHealth>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (asteroidHealth < 0 && asteroidLevel != 1)
        { 
            asteroidLevel -= 1;
            asteroidHealth = asteroidLevel;
            SplitAsteroid();
            AsteroidSize();
        }

        if (asteroidHealth < 0 && asteroidLevel == 1)
        {

            Destroy(this.gameObject);

        }

        Debug.Log(playerHealthScript.damageAsteroid);
    }

    void AsteroidSize()
    {   
        Vector3 asteroidSize = new Vector2(10, 10) * asteroidLevel;
        asteroidTransf.localScale = asteroidSize;   
    }

    void SplitAsteroid()
    {
//        for (int i = 0; i < 2; i++)
//        {           
        GameObject asteroidChunk = GameObject.Instantiate(asteroids[Random.Range(0, 2)], asteroidTransf.position, asteroidTransf.rotation);
        PolygonCollider2D chunkCol = asteroidChunk.GetComponent<PolygonCollider2D>();

        Physics2D.IgnoreCollision(chunkCol, asteroidCol);

        Asteroid chunkScript = asteroidChunk.GetComponent<Asteroid>();
        Rigidbody2D chunkRgbd = asteroidChunk.GetComponent<Rigidbody2D>();

        chunkRgbd.velocity = Random.insideUnitCircle.normalized * Random.Range(2, 5);
        rgbd.velocity = Random.insideUnitCircle.normalized * Random.Range(2, 5);
        chunkScript.asteroidLevel = asteroidLevel;
        //        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "player") {
            Debug.Log("t");
            if (playerHealthScript.damageAsteroid == true) {
                Debug.Log("d");
                StartCoroutine(Flicker());
                asteroidHealth = -1;
            }
        }

        if (other.collider.tag == "Bullet")
        {
            StartCoroutine(Flicker());
            asteroidHealth -= bulletDamage;
        }

        if (other.collider.tag == "Asteroid")
        {
            Physics2D.IgnoreCollision(other.collider, asteroidCol);
        }      
    }

    IEnumerator Flicker() {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;

        yield return null;
    }

}
