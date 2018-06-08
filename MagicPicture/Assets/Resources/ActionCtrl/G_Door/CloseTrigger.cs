using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTrigger : MonoBehaviour {

    [SerializeField] DoorAction doorAction;
    [SerializeField] GameObject cantBack;
    
    public bool closeFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //---------------------------
    // Playerが感圧板に触れたら
    void OnTriggerStay(Collider col)
    {
        closeFlag = true;           // ドアを開けれないようにする
        doorAction.Close();         // ドアを閉める        
        cantBack.SetActive(true);   // 通れないようにする(挟まり防止)
    }

    void OnCollision(Collision col)
    {
        closeFlag = true;
        doorAction.Close();
        cantBack.SetActive(true);
    }


    //-----------------------------
    // Playerが感圧板から離れたら
    void OnTriggerExit(Collider col)
    {
        if (doorAction.resetFlag)

        closeFlag = false;          // ドアを開けれるようにする
        cantBack.SetActive(false);  // 通れるようにする
    }

    void OnCollisionExit(Collision col)
    {
        if (doorAction.resetFlag)

        closeFlag = false;
        cantBack.SetActive(false);
    }
}