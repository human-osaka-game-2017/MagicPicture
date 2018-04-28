using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {


    Vector3 m_Pos;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
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
        m_Pos.x = player.transform.position.x;
        m_Pos.y = player.transform.position.y + 4.5f;  //元2.5
        m_Pos.z = player.transform.position.z - 3.5f;  //元-4.5
    }
}
