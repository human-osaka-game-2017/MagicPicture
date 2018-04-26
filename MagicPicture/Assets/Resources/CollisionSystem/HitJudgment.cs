using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitJudgment : MonoBehaviour {
    

    public static bool HitFloorFlag;
    public static bool HitFallPlaneFlag;
    public static bool HitEnemyFlag;

    private string FloorTagName;
    private string FallTagName;
    private string EnemyTagName;


    // Use this for initialization
    void Start () {
        FloorTagName = "FloorTag";
        FallTagName  = "FallTag";
        EnemyTagName = "EnemyTag";
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //=============
    // あたり判定
    //=============
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == FloorTagName) {
            HitFloorFlag = true;
        }
        
        if (col.gameObject.tag == FallTagName) {
            HitFallPlaneFlag = true;
        }
    }
    
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == FloorTagName) {
            HitFloorFlag = false;
        }
        
        if (col.gameObject.tag == FallTagName) {
            HitFallPlaneFlag = false;
        }
    }


    void OnTriggerStay(Collider col)
    {
        // 回転中は敵とのあたり判定を無効化
        if (PlayerRotation.RotationFlag == false) {
            if (col.gameObject.tag == EnemyTagName) {
                HitEnemyFlag = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        // こっちも回転中は敵とのあたり判定を無効化
        if (PlayerRotation.RotationFlag == false) {
            if (col.gameObject.tag == EnemyTagName) {
                HitEnemyFlag = false;
            }
        }
    }
}