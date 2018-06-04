using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel_1 : MonoBehaviour {

    [SerializeField] OpenDoor_1 button;
    [SerializeField] GameObject doorR_1;
    [SerializeField] GameObject doorL_1;
    [SerializeField] GameObject cantBack_1;

    private bool     stepPanelFlag;
    private float    closeRange;
    private Collider cantBackCol;

    // Use this for initialization
    void Start () {
        cantBackCol = cantBack_1.GetComponent<Collider>();
        closeRange += button.closeRange + 0.01f;
    }
	
	// Update is called once per frame
	void Update () {

        if (doorR_1.transform.localPosition.x < closeRange) {
            stepPanelFlag = false;

            // 元に戻す
            cantBackCol.isTrigger = true;
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
        if (col.transform.name == "PlayerCol") {
            stepPanelFlag = true;
            
            // ドアに挟まれない用に戻れなくする
            cantBackCol.isTrigger = false;
        }
    }
}