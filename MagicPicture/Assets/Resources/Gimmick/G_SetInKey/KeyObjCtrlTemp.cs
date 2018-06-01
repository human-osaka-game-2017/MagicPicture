using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjCtrlTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey("t")) {
            transform.Translate(0, 0, 0.2f);
        }
        if (Input.GetKey("g")) {
            transform.Translate(0, 0, -0.2f);
        }
        if (Input.GetKey("h")) {
            transform.Translate(0.2f, 0, 0);
        }
        if (Input.GetKey("f")) {
            transform.Translate(-0.2f, 0, 0);
        }

        if (Input.GetKey("i")) {
            transform.Rotate(2, 0, 0);
        }
        if (Input.GetKey("k")) {
            transform.Rotate(-2, 0, 0);
        }
        if (Input.GetKey("j")) {
            transform.Rotate(0, -2, 0);
        }
        if (Input.GetKey("l")) {
            transform.Rotate(0, 2, 0);
        }
    }
}
