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
        float playerTop = player.transform.position.y + playerHeight / 2 + 0.01f;
        float playerBot = player.transform.position.y - playerHeight / 2 - 0.01f;

        float platformHeight = coll.bounds.size.y;
        float platformTop = transform.position.y + platformHeight / 2 + 0.01f;
        float platformBot = transform.position.y - platformHeight / 2 - 0.01f;

        bool drop = false;
                
        // If player drops all the way through platform, stop dropping.
        if (playerTop < platformBot)
        {
            // If the player is not dropping, then drop will remain false.
            player.GetComponent<PlayerMovement>().dropping = false;
        }
        // If player is below the platform, but not all the way through.
        else if (playerBot < transform.position.y)
        {
            drop = true;
        }

        // If the player is moving upwards, don't collide
        if (playerRigidbody.velocity.y > 0.1f)
        {
            drop = true;
        }
        //If the player is dropping through a platform, don't collide.
        else if (player.GetComponent<PlayerMovement>().dropping)
        {
            drop = true;
        }

        // If drop was never turned to true above, then collide.
        // If drop was turned to true above, don't collide.
        Physics2D.IgnoreCollision(coll, playerColl, drop);
    }
}
