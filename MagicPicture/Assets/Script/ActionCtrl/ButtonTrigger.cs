using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {
    
    [SerializeField] ButtonTriggerCtrl buttonCtrl;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        if (buttonCtrl.onCount == buttonCtrl.buttonNum) {
            buttonCtrl.actionCtrl.Action();
        }
        else {
            buttonCtrl.actionCtrl.Reset();
        }
    }


    // ボタンが押されたら
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Player") {
            buttonCtrl.onCount++;
        }
    }

    // ボタンが離されたら
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name != "Player") {
            if (buttonCtrl.onCount > 0) {
                buttonCtrl.onCount--;
            }
        }
    }
}