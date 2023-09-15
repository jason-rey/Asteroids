using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 4;
    public float invincibleTime = 2;
    public bool damageAsteroid = true;
    public GameObject explosionEffect;
    public bool isDead = false;
    public playerController playerControllerScript;
    public GameObject fireEmitter;
    public GameObject bulletLocation;

    private Rigidbody2D rgbd;
    private bool isInvincible;
    private SpriteRenderer playerSprite;
    private PolygonCollider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = this.GetComponent<PolygonCollider2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        rgbd = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {          
            StartCoroutine(Explode());            
            health = -1;
            isDead = true;
        }

        else if (health > 0)
        {
            isDead = false;
        }

        if (isDead)
        {
            playerCollider.enabled = false;
            playerSprite.enabled = false;
            rgbd.bodyType = RigidbodyType2D.Kinematic;
        }

        else
        {
            playerCollider.enabled = true;
            playerSprite.enabled = true;
            rgbd.bodyType = RigidbodyType2D.Dynamic;
        }

        if (health > 0)
        {
            if (isInvincible)
            {
                playerSprite.enabled = false;
                damageAsteroid = false;
                if (invincibleTime > 0)
                {
                    InvokeRepeating("InvincibleFlicker", 0.01f, 0.5f);
                    invincibleTime -= Time.deltaTime;
                }

                if (invincibleTime < 0)
                {
                    playerSprite.enabled = true;
                    damageAsteroid = true;
                    invincibleTime = 2;
                    CancelInvoke();
                    isInvincible = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            if (!isInvincible)
            {
                health -= 1;
                isInvincible = true;               
            }            
        }
    }

    void InvincibleFlicker()
    {
        playerSprite.enabled = true;
    }

    IEnumerator Explode()
    {
        fireEmitter.GetComponent<Animator>().SetBool("isMoving", false);
        bulletLocation.GetComponent<Animator>().SetBool("New Bool", false);
        this.GetComponent<playerController>().CancelInvoke();
        this.GetComponent<playerController>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;

        GameObject explosionObject = GameObject.Instantiate(explosionEffect, this.transform.position, this.transform.rotation);
        Animator objectAnimator = explosionObject.GetComponent<Animator>();
        objectAnimator.SetBool("canExplode", true);

        yield return new WaitForSeconds(0.5f);

        objectAnimator.SetBool("canExplode", false);
     
    }

    public void NewGame()
    {
        this.GetComponent<playerController>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.transform.position = Vector2.zero;
        health = 4;
        fireEmitter.SetActive(true);
        bulletLocation.SetActive(true);
        playerControllerScript.enabled = true;

        GameObject[] oldAsteroids;

        oldAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i = 0; i < oldAsteroids.Length; i++)
        {
            Destroy(oldAsteroids[i]);
        }
    }
}
