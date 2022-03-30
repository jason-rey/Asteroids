using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrapAround : MonoBehaviour
{
    private float screenBoundsX = 25;
    private float screenBoundsY = 15;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        if (playerTransform.position.y > screenBoundsY)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, -screenBoundsY);
        }

        if (playerTransform.position.y < -screenBoundsY)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, screenBoundsY);
        }

        if (playerTransform.position.x > screenBoundsX)
        {
            playerTransform.position = new Vector2(-screenBoundsX, playerTransform.position.y);
        }

        if (playerTransform.position.x < -screenBoundsX)
        {
            playerTransform.position = new Vector2(screenBoundsX,playerTransform.position.y);
        }
    }
}
