using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTrigger : MonoBehaviour {

    [SerializeField] ActionCtrl actionCtrl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //-------------------
    // 感圧板を踏んだら
    void OnTriggerStay(Collider col)
    {
        // 現像前のobjectに当たってもスルー(layerの2)
        if (col.gameObject.layer != 2) {
            actionCtrl.Action();
        }
    }
    
    //---------------------
    // 感圧板から離れたら
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer != 2) {
            actionCtrl.Reset();
        }
    }
}