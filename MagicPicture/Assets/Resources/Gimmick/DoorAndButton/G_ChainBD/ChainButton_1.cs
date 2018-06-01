using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainButton_1 : MonoBehaviour {

    [SerializeField] ChainBDCtrl chainBDCtrl;

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

            chainBDCtrl.closeFlag = false;
        }
    }
}