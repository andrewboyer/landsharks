using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool shadow;

    public float runSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;
    private bool jumping = false;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {

        //horizontal movement:
        float velo = 0f;

        if (shadow) {

            if (Input.GetKey(KeyCode.A))
                velo -= runSpeed;
            if (Input.GetKey(KeyCode.D))
                velo += runSpeed;
            

        } else {

            if (Input.GetKey(KeyCode.LeftArrow))
                velo -= runSpeed;
            if (Input.GetKey(KeyCode.RightArrow))
                velo += runSpeed;

        }

        rb.velocity = new Vector2(velo, rb.velocity.y);
        anim.SetFloat("Velo", velo); // Set speed for animator.

        //If the player has jumped and reaches the ground, let the player jump.
        if (jumping && OnGround()) jumping = false;

        //If the player is not jumping and the jump button is pressed:
        if (shadow) {

            if (!jumping && Input.GetKeyDown(KeyCode.W))
            {
                jumping = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
            }

        } else {

            if (!jumping && Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumping = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
            }

        }


        // Dropping through platforms:
        // TODO: Begin Collisions after the player falls through the platform.
        if (OnGround()
            && OnGround().collider.gameObject.layer == LayerMask.NameToLayer("Drop Platform"))
        {
            GameObject platform = OnGround().collider.gameObject;
            if (shadow && Input.GetKeyDown(KeyCode.S))
            {
                platform.GetComponent<PlatformCollider>().shadowDrop = true;
            }
            else if (!shadow && Input.GetKeyDown(KeyCode.DownArrow))
            {
                platform.GetComponent<PlatformCollider>().playerDrop = true;
            }
        }
    }

    // Cast a line to check whether the player is on the ground
    RaycastHit2D OnGround () {

		//find width and height of character
		BoxCollider2D coll = GetComponent<BoxCollider2D> ();
		Vector2 pos = transform.position;
		float width = coll.bounds.size.x;
		float height = coll.bounds.size.y;

		//the ground check draws a line right underneath the player
		//if there is a collider on that line, the player is on something
		//and therefore can jump
		//p1 and p2 are the ends of that line
		Vector2 p1 = new Vector2 (pos.x - width / 2f + 0.01f, pos.y - height / 2f - 0.02f);
		Vector2 p2 = new Vector2 (pos.x + width / 2f - 0.01f, pos.y - height / 2f - 0.02f);

		return Physics2D.Linecast (p1, p2);
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
        Debug.DrawLine(new Vector3(p1.x, p1.y, 0), new Vector3(p2.x, p2.y, 0));

        return Physics2D.Linecast(p1, p2);
    }

}
