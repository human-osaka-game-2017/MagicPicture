using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitJudgment : MonoBehaviour {

    
    public static bool HitFloorFlag;
    public static bool HitFallFlag;
    public static bool HitFallGameOverPlaneFlag;
    public static bool HitEnemyFlag;

    private string      FloorTagName;
    private string      FallTagName;
    private string      FallGameOverTagName;    
    private string      EnemyTagName;
    private Collider    m_ObjectCollider;


    // Use this for initialization
    void Start () {
        FloorTagName            = "FloorTag";
        FallTagName             = "FallTag";
        FallGameOverTagName     = "FallGameOverTag";
        EnemyTagName            = "EnemyTag";

        m_ObjectCollider = GetComponent<Collider>();
        Debug.Log(m_ObjectCollider.isTrigger);
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
    }
    
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == FloorTagName) {
            HitFloorFlag = false;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == FallGameOverTagName) {
            HitFallGameOverPlaneFlag = true;

            m_ObjectCollider.isTrigger = true;
            Debug.Log(m_ObjectCollider.isTrigger);
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

        if (col.gameObject.tag == FallTagName) {
            HitFallFlag = true;
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

        if (col.gameObject.tag == FallGameOverTagName) {
            HitFallGameOverPlaneFlag = false;
        }

        if (col.gameObject.tag == FallTagName) {
            HitFallFlag = false;
        }
    }
}