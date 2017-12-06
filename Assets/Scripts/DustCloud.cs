using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour {

    public ParticleSystem dustcloud;

    // Use this for initialization
    void Start () {
        
        dustcloud.Play();
        Object.Destroy(gameObject, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
