using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHit : MonoBehaviour {


    Vector3 m_Pos;

    public static bool  BackHitFlag;

    private string      WallTagName;


    // Use this for initialization
    void Start () {
        WallTagName = "WallTag";

        m_Pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        InterlockingMovement();

        transform.position = m_Pos;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            BackHitFlag = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == WallTagName) {
            BackHitFlag = false;
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
        m_Pos.y = player.transform.position.y +0.25f;//-0.25
        m_Pos.z = player.transform.position.z - 1.0f;
    }
}
