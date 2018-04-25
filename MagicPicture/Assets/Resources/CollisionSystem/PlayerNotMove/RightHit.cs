using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHit : MonoBehaviour {


    public static bool  RightHitFlag;

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
            RightHitFlag = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            RightHitFlag = false;
        }
    }
}
