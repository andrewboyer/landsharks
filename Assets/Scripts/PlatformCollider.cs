using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public GameObject human;
    public GameObject shadow;

    private Collider2D coll;

    // Use this for initialization.
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // At the start of every physics frame, check whether the players collide
    private void FixedUpdate()
    {
        CheckCollisions(human);
        //CheckCollisions(shadow);

        
    }

    private void CheckCollisions(GameObject player)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        Collider2D playerColl = player.GetComponent<Collider2D>();

        // Calculate the top of the player and the bottom of the platform.
        float playerHeight = playerColl.bounds.size.y;
        float playerY = player.transform.position.y + playerHeight / 2 + 0.01f;

        float platformHeight = coll.bounds.size.y;
        float platformY = transform.position.y - platformHeight / 2 - 0.01f;
                
        // If the player is below the platform, the player stops dropping
        if (playerY < platformY)
        {
            player.GetComponent<PlayerMovement>().dropping = false;
        }

        // If the player is moving upwards, don't collide
        if (playerRigidbody.velocity.y > 0.1f)
        {
            Physics2D.IgnoreCollision(coll, playerColl, true);
        }
        //If the player is dropping through a platform, don't collide
        else if (player.GetComponent<PlayerMovement>().dropping)
        {
            Physics2D.IgnoreCollision(coll, playerColl, true);
        }
        else
        {
            Physics2D.IgnoreCollision(coll, playerColl, false);
        }
    }
}
