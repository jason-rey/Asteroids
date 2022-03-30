using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public gameUI uiScript;
    public float maxBulletRange = 100;
    // Start is called before the first frame update
    void Start()
    {
        uiScript = GameObject.Find("Canvas").GetComponent<gameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(new Vector2(0,0),this.transform.position)>maxBulletRange)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Asteroid")
        {
            GameObject hitAsteroid = other.collider.gameObject;
            Asteroid asteroid = hitAsteroid.GetComponent<Asteroid>();


            if (asteroid.asteroidHealth == 0)
            {
                switch (asteroid.asteroidLevel)
                {
                    case 4:
                        uiScript.playerScore += 10;
                        break;
                    case 3:
                        uiScript.playerScore += 20;
                        break;
                    case 2:
                        uiScript.playerScore += 50;
                        break;
                    case 1:
                        uiScript.playerScore += 100;
                        break;
               }
            }
        }

        if (other.collider.tag != "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
