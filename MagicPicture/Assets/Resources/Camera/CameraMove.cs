using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {


    Vector3 m_Pos;

    // Use this for initialization
    void Start()
    {
        m_Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        InterlockingMovement();

        transform.position = m_Pos;
    }


    //----------------------------------------
    // PlayerAngleの位置をplayerに連動させる
    //----------------------------------------
    void InterlockingMovement()
    {
        GameObject player;

        player = GameObject.Find("Player");

        m_Pos.x = player.transform.position.x;
        m_Pos.y = player.transform.position.y + 2.5f;
        m_Pos.z = player.transform.position.z - 4.5f;
    }
}
