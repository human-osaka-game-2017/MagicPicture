using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHit : MonoBehaviour {


    public static bool  BackHitFlag;

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
            BackHitFlag = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            BackHitFlag = false;
        }
    }
}
