using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhyThis : MonoBehaviour {

	private Text text;
	public float tim;

	// Use this for initialization
	void Start () {

		text = GetComponent<Text> ();
		ColorChange ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ColorChange() {

		text.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1f);
		Invoke ("ColorChange", tim);

	}
}
