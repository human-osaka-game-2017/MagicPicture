using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel_1 : MonoBehaviour {

    [SerializeField] DoorR_1    doorR_1;
    [SerializeField] DoorL_1    doorL_1;
    [SerializeField] OpenDoor_1 button;

    private bool stepPanelFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (doorR_1.transform.localPosition.x < 0.76f) {
            stepPanelFlag = false;
        }
        
        // ClosePanelが踏まれたら
        if (stepPanelFlag) {
            button.doorState = 2;
        }
	}


    //===============================
    // PlayerがClosePanelを踏んだら
    //===============================
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == "Player") {
            stepPanelFlag = true;
        }
    }
}