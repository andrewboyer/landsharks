using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {

	private Animator anim;

	public bool isShadow;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		bool moveLeft;
		bool moveRight;
		bool moving;

		if (isShadow) {
			moveLeft = Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow);
			moveRight = Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow);
		} else {
			moveLeft = Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D);
			moveRight = Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A);
		}

		moving = moveLeft || moveRight;

		anim.SetBool ("running", moving);

		if (moveLeft)
			anim.SetBool ("faceRight", false);
		else if (moveRight)
			anim.SetBool ("faceRight", true);
		
	}
}
