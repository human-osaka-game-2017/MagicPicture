using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaEmptyRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RotHitJudgment();
    }
    
    //------------------------------
    // 向く方向にBoxを回転移動する
    void RotHitJudgment()
    {
        if (CharaMove.directionMove == 1) {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (CharaMove.directionMove == 2) {
            this.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        if (CharaMove.directionMove == 3) {
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        if (CharaMove.directionMove == 4) {
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }
}