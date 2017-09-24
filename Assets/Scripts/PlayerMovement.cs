using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool shadow;

	private Rigidbody2D rb;

	public float runSpeed;
	public float jumpForce;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		//horizontal movement:
		float velo = 0f;

		if (shadow) {

			if (Input.GetKey (KeyCode.A))
				velo -= runSpeed;
			if (Input.GetKey (KeyCode.D))
				velo += runSpeed;

		} else {

			if (Input.GetKey (KeyCode.LeftArrow))
				velo -= runSpeed;
			if (Input.GetKey (KeyCode.RightArrow))
				velo += runSpeed;

		}

		rb.velocity = new Vector2 (velo, rb.velocity.y);

		//jumping:
		if (shadow) {

			if (Input.GetKeyDown (KeyCode.W) && OnGround())
				rb.AddForce (Vector2.up * jumpForce);

		} else {

			if (Input.GetKeyDown (KeyCode.UpArrow) && OnGround ())
				rb.AddForce (Vector2.up * jumpForce);

		}


        // Dropping through platforms:
        // TODO: Begin Collisions after the player falls through the platform.
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            && OnGround())
        {
            if (OnGround().collider.gameObject.layer == LayerMask.NameToLayer("Drop Platform"))
            {
                BoxCollider2D coll = GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(coll, OnGround().collider);
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

}
