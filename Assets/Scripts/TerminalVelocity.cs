using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalVelocity : MonoBehaviour {

	private Rigidbody2D rb;
	public float maxVelo;

	void Start () {

		rb = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {

		Vector2 velo = rb.velocity;
		if (Mathf.Abs (velo.y) > maxVelo) {

			if (velo.y < 0)
				rb.velocity = new Vector2 (rb.velocity.x, -maxVelo);
			else
				rb.velocity = new Vector2 (rb.velocity.x, maxVelo);

		}
		
	}

}
