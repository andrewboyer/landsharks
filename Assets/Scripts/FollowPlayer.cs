using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject character;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		offset = transform.position - character.transform.position;
		
	}
	
	// LateUpdate is called once per frame after other actions
	void LateUpdate () {

		transform.position = character.transform.position + offset;

	}
}
