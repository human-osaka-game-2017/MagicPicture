using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpProcessing : MonoBehaviour {
    
    public int state;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (state == 1) {
            transform.Rotate(0, 5, 0);

            state = 0;
        }
        if (state == 2) {
            transform.Rotate(0, -10, 0);

            state = 0;
        }
    }
}