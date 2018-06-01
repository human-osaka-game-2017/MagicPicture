using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpProcessing : MonoBehaviour {
    
    public bool state;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (state) {
            transform.Rotate(0, 5, 0);

            state = false;
        }
    }
}