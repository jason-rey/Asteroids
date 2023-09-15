using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMoreAsteroids : MonoBehaviour
{
    public GameObject[] asteroids;
    public int desiredAsteroidCount = 0;
    public GameObject[] activeAsteroids;

    private bool canSpawn = false;
    private int screenBoundsX = 31;
    private int screenBoundsY = 17;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        
        if (activeAsteroids.Length > 0)
        {
            desiredAsteroidCount = 0;
        }

        if (activeAsteroids.Length == 0)
        {
            desiredAsteroidCount = Random.Range(10, 15);
            canSpawn = true;
        }

        if (canSpawn)
        {
            SpawnAsteroids();
            canSpawn = false;
        }
    }

    public void SpawnAsteroids()
    {
        for (int i = 0; i < desiredAsteroidCount; i++)
        {
            Vector2 asteroidPos = new Vector2(Random.Range(-screenBoundsX, screenBoundsX), Random.Range(-screenBoundsY, screenBoundsY));
            GameObject asteroid = GameObject.Instantiate(asteroids[Random.Range(0, asteroids.Length)], asteroidPos, this.transform.rotation);
            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            Rigidbody2D asteroidRgbd = asteroid.GetComponent<Rigidbody2D>();

            asteroidScript.asteroidLevel = Random.Range(2, 4);


            asteroidRgbd.velocity = Random.insideUnitCircle.normalized * Random.Range(2, 5);

        }
    }
}
