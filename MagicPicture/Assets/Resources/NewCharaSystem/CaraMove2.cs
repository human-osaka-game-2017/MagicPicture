using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaraMove2 : MonoBehaviour {

    Vector3     pos;
    Rigidbody   RigidBodyCompnent;

    // Use this for initialization
    void Start () {
        RigidBodyCompnent = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("w")) {
            pos.z = 5;
        }
        if (Input.GetKey("s")) {
            pos.z = -5;
        }
        if (Input.GetKey("a")) {
            pos.x = -5;
        }
        if (Input.GetKey("d")) {
            pos.x = 5;
        }
        pos.y = -3;


        if (!Input.GetKey("w") && !Input.GetKey("s")) {
            pos.z = 0;
        }
        if (!Input.GetKey("a") && !Input.GetKey("d")) {
            pos.x = 0;
        }
        
        
        RigidBodyCompnent.AddForce(pos);
    }
}