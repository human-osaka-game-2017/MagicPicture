using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxChara : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    

    // これであたり判定できてるで！
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall") {
            //if (CharaMove.moveKeyNum == 1) CharaMove.moveKeyNum = 2;
            //if (CharaMove.moveKeyNum == 2) CharaMove.moveKeyNum = 1;
            //if (CharaMove.moveKeyNum == 3) CharaMove.moveKeyNum = 4;
            //if (CharaMove.moveKeyNum == 4) CharaMove.moveKeyNum = 3;

            CharaMove.hitFlag = true;
        }

        if (col.gameObject.tag == "Stairs") {
            //if (CharaMove.moveKeyNum == 4) CharaMove.moveKeyNum = 8;
        }
    }
}