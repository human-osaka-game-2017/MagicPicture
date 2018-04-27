using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontHit : MonoBehaviour {


    Vector3 m_Pos;

    public static bool  FrontHitFlag;

    private string      WallTagName;


    // Use this for initialization
    void Start () {
        WallTagName = "WallTag";
    }
	
	// Update is called once per frame
	void Update () {
        InterlockingMovement();

        transform.position = m_Pos;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            FrontHitFlag = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            FrontHitFlag = false;
        }
    }


    //-------------------------------
    // プレイヤーの位置に連動させる
    //-------------------------------
    void InterlockingMovement()
    {
        GameObject player;

        player = GameObject.Find("Player");

        m_Pos.x = player.transform.position.x;
        m_Pos.y = player.transform.position.y;
        m_Pos.z = player.transform.position.z + 1.0f;
    }
}
