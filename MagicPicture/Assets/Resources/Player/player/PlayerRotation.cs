using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    Vector3             rotation;
    
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update() {        
        Rotation();
    }


    void Rotation()
    {
        if (Input.GetKey("left")) {
            rotation.y = -1.5f;
        }
        if (Input.GetKey("right")) {
            rotation.y = 1.5f;
        }

        if (!Input.GetKey("left") && !Input.GetKey("right")) {
            rotation.y = 0;
        }
        
        transform.Rotate(rotation);
    }
}