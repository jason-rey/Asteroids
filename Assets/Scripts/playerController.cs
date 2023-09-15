using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float thrustForce;
    public GameObject bullet;
    public GameObject fireLocation;
    public float bulletForce;
    public Animator playerAnim;
    public Animator bulletAnim;
    public float fireRate;
    public playerHealth playerHealthScript;

    private bool canFire;
    private Vector3 direction;
    private Rigidbody2D rgbd;
    private float thrustInput;
    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = this.GetComponent<Transform>();
        rgbd = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealthScript.isDead)
        {

            this.enabled = true;

            direction = rgbd.transform.up.normalized;
            thrustInput = Input.GetAxisRaw("Vertical");

            if (thrustInput > 0)
            {
                playerAnim.SetBool("isMoving", true);
            }

            else if (thrustInput == 0)
            {
                playerAnim.SetBool("isMoving", false);
            }

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            playerPos.Rotate(Vector3.forward * -(horizontalInput * rotationSpeed * Time.deltaTime));


            if (Input.GetKeyDown(KeyCode.X))
            {
                InvokeRepeating("FireBullet", 0, fireRate);
            }

            if (Input.GetKey(KeyCode.X))
            {
                bulletAnim.SetBool("New Bool", true);
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                bulletAnim.SetBool("New Bool", false);
                CancelInvoke();
            }
        }

        if (playerHealthScript.isDead)
        {
            CancelInvoke();
        }


    }


    void FixedUpdate()
    {
        MoveShip(thrustInput,direction);
    }

    void FireBullet()
    {
        if (this.enabled)
        {
            GameObject tempBullet = GameObject.Instantiate(bullet);
            tempBullet.transform.position = fireLocation.transform.position;
            tempBullet.transform.rotation = playerPos.rotation;
            Rigidbody2D bulletBody = tempBullet.GetComponent<Rigidbody2D>();
            bulletBody.velocity += ((Vector2)direction * bulletForce);
        }

    }

    void MoveShip(float input, Vector3 direction)
    {
        if (input > 0)
        {
            rgbd.AddForce(direction * thrustForce);
        }

        if (input < 0)
        {
            rgbd.AddForce(-direction * (thrustForce/2));
        }
    }
}
