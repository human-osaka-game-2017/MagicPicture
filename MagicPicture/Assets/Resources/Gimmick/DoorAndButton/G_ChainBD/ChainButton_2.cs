using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainButton_2 : MonoBehaviour {

    public bool onButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Player") {
            onButton = true;
        }
    }


    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name != "Player") {
            onButton = false;
        }
    }
}