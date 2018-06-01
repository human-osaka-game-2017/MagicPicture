using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainClosePanel : MonoBehaviour {

    [SerializeField] GameObject  cantBack;
    [SerializeField] ChainBDCtrl ctrl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //===============================
    // PlayerがClosePanelに触れたら
    //===============================
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player") {
            ctrl.closeFlag = true;

            cantBack.GetComponent<Collider>().isTrigger = false;
        }
    }
}