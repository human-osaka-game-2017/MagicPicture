using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGimmickSpear : MonoBehaviour {

    public bool onFlag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //===========================
    // Playerが感圧板に触れたら
    //===========================
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Player") {
            onFlag = true;
        }
    }


    //=============================
    // Playerが感圧板から離れたら
    //=============================
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player") {
            onFlag = false;
        }
    }


    public bool GetOnFlag()
    {
        return onFlag;
    }
}