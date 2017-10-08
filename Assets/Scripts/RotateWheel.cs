using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float rotateSpeed = 25f;

    // Update is called once per frame.
    void Update()
    {
        // This script rotates the entire ferris wheel GameObject.
        // The children will all rotate with it.
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
