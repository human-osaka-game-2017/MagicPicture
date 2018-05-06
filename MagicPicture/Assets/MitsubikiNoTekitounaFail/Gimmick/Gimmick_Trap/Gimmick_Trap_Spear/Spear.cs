using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }
}
