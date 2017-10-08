using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float friction;


    // Use this for initialization.
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        friction = rb2d.sharedMaterial.friction;
    }

    void FixedUpdate()
    {
        float velo = rb2d.velocity.x;

        //If the player hits a wall while in the air, slide along the wall.
        if (!OnGround() && OnWall(velo < 0))
        {
            rb2d.sharedMaterial.friction = 0;
        }
        else
            rb2d.sharedMaterial.friction = friction;
    }

    // Cast a line to check whether the player is on the ground
    RaycastHit2D OnGround()
    {

        //find width and height of character
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        Vector2 pos = transform.position;
        float width = coll.bounds.size.x;
        float height = coll.bounds.size.y;

        //the ground check draws a line right underneath the player
        //if there is a collider on that line, the player is on something
        //and therefore can jump
        //p1 and p2 are the ends of that line
        Vector2 p1 = new Vector2(pos.x - width / 2f + 0.01f, pos.y - height / 2f - 0.02f);
        Vector2 p2 = new Vector2(pos.x + width / 2f - 0.01f, pos.y - height / 2f - 0.02f);

        return Physics2D.Linecast(p1, p2);
    }

    RaycastHit2D OnWall(bool left)
    {
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        float width = coll.bounds.size.x;
        float height = coll.bounds.size.y;
        Vector2 pos = transform.position;

        int bounds = 1;
        if (left) bounds = -1;

        // The x coordinate of the player's side.
        float boundX = bounds * (width / 2f + 0.01f);


        // Draw a line on the appropriate side of the player.
        Vector2 p1 = new Vector2(pos.x + boundX, pos.y - height / 2f - 0.02f);
        Vector2 p2 = new Vector2(pos.x + boundX, pos.y + height / 2f + 0.02f);

        return Physics2D.Linecast(p1, p2);
    }
}
