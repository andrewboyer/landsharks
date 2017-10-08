using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientCart : MonoBehaviour
{   
    // Update is called once per frame.
    void Update()
    {
        // This script keeps the global rotation of carts normalized.
        // No matter the parent's rotation, the carts will stay upright.
        transform.rotation = Quaternion.identity;
    }
}
