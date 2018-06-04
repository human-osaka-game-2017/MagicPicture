using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour {

    [SerializeField] GameObject     cantBack;
    [SerializeField] OnTogetherCtrl ctrl;

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
        if (col.gameObject.name == "PlayerCol") {
            ctrl.closeFlag = true;

            cantBack.GetComponent<Collider>().isTrigger = false;
        }
    }
}