using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject person;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		offset = transform.position - person.transform.position;
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		transform.position = person.transform.position + offset;
	}
}
