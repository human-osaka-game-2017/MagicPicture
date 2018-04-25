using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHit : MonoBehaviour {


    public static bool  LeftHitFlag;

    private string      WallTagName;


    // Use this for initialization
    void Start () {
        WallTagName = "WallTag";
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            LeftHitFlag = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            LeftHitFlag = false;
        }
    }
}
