using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {

	public GameObject endGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.GetComponent<PlayerMovement> () != null) { //if something has player movement, it's probably a player

			Debug.Log ("THE GAME IS OVER AND YOUR CREDIT CARD INFORMATION IS MINEEEEE");
			endGame.SetActive(true);

		}

	}

}
