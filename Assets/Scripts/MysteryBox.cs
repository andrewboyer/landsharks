using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour {

	public float timer = 1.0f;
	public bool alive = true;

    private ParticleSystem particle;
    private float particleScale;

	// Use this for initialization
	void Start () {
        particle = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!alive) {
			
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && alive) {
			alive = false;
			GetComponent<BoxCollider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().enabled = false;
            particle.Stop();
			StartCoroutine(EnableBox(timer));
		}
	}

	IEnumerator EnableBox(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().enabled = true;
        particle.Play();
		alive = true;
	}

}
