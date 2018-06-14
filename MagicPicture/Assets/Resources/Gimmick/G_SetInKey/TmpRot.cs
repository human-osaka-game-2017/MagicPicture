using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("f")) {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey("g")) {
            transform.Rotate(0, 1, 0);
        }
	}
}