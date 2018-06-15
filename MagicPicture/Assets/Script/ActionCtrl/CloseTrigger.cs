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

    void FixedUpdate() {

        if (closeFlag) {
            doorAction.Close();     // ドアを閉めるを呼ぶ
        }
    }

    //---------------------------
    // Playerが感圧板に触れたら
    void OnTriggerEnter(Collider col)
    {
        closeFlag = true;   // ドアを開けれないようにする
    }

    //---------------------------------
    // Player以外でも感圧板に触れたら
    void OnCollisionEnter(Collision col)
    {
        closeFlag = true;
    }
}