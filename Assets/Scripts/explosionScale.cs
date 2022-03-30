using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScale : MonoBehaviour
{
    public Asteroid asteroidScript;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(12, 13);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
