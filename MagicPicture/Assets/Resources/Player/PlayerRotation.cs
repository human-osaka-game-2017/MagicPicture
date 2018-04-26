﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private int         RotationDirection;
    private bool        CantRotationFlag;
    private float       playerEmpAngleY;
    private float       tempPlayerEmpAngleY;    
    private Vector3     m_RotationAngle;
    private Quaternion  m_Rotation;
    public static bool  RotationFlag = false;

    
    // Use this for initialization
    void Start()
    {
        m_RotationAngle = m_Rotation.eulerAngles;
        CantRotationFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.isMoveng == false) {
            CantRotationFlag = false;
        }

        if (PlayerMove.gameOverFlag == false) {
            if (RotationFlag == false) {
                SetPlayerRotation();
            }
            
            if (RotationDirection == 0) {
                SetRotationDirection();
            }
            else {
                PlayerRotating();
            }
        }
    }


    //=================
    // プレイヤー回転
    //-----------------------
    // じんわり回転方向決定
    void SetRotationDirection()
    {
        if (PlayerMove.isMoveng == false) {
            
            if (Input.GetKeyDown("up")) {
                RotationDirection = 1;
            }
            if (Input.GetKeyDown("down")) {
                RotationDirection = 2;
            }
            if (Input.GetKeyDown("right")) {
                RotationDirection = 3;
            }
            if (Input.GetKeyDown("left")) {
                RotationDirection = 4;
            }
        }
    }


    //-------------------------
    // プレイヤーの回転値代入
    //-------------------------
    void SetPlayerRotation()
    {
        if (CantRotationFlag == false) {

            if (Input.GetKeyDown("w")) {

                tempPlayerEmpAngleY = 0.0f;
                PlayerRotationUpdate(playerEmpAngleY = 0.0f);
            }
            if (Input.GetKeyDown("s")) {

                tempPlayerEmpAngleY = 180.0f;
                PlayerRotationUpdate(playerEmpAngleY = 180.0f);
            }
            if (Input.GetKeyDown("d")) {

                tempPlayerEmpAngleY = 90.0f;
                PlayerRotationUpdate(playerEmpAngleY = 90.0f);
            }
            if (Input.GetKeyDown("a")) {

                tempPlayerEmpAngleY = -90.0f;
                PlayerRotationUpdate(playerEmpAngleY = -90.0f);
            }

            if (PlayerMove.isMoveng) {
                CantRotationFlag = true;
            }
        }
    }

    //---------------
    // じんわり回転
    //---------------
    void PlayerRotating()
    {
        RotationFlag = true;

        switch (RotationDirection)
        {
            case 1:
                PlayerRotationAll(90.0f, -3.0f);
                PlayerRotationAll(-90.0f, 3.0f);
                PlayerRotationAll(180.0f, -3.0f);

                RotationEnd(0.0f, 0.0f);
                break;

            case 2:
                PlayerRotationAll(0.0f, 3.0f);
                PlayerRotationAll(90.0f, 3.0f);
                PlayerRotationAll(-90.0f, -3.0f);

                RotationEnd(180.0f, 180.0f);
                RotationEnd(-180.0f, 180.0f);
                break;

            case 3:
                PlayerRotationAll(0.0f, 3.0f);
                PlayerRotationAll(-90.0f, 3.0f);
                PlayerRotationAll(180.0f, -3.0f);

                RotationEnd(90.0f, 90.0f);
                break;

            case 4:
                PlayerRotationAll(0.0f, -3.0f);
                PlayerRotationAll(90.0f, -3.0f);
                PlayerRotationAll(180.0f, 3.0f);

                RotationEnd(-90.0f, -90.0f);
                RotationEnd(270.0f, -90.0f);
                break;
        }
    }


    //-----------------------------------------
    // 時計回り、反時計回りにじんわり回転する
    //-----------------------------------------
    void PlayerRotationAll(float playerAngY, float tempPlayerAng)
    {
        if (playerEmpAngleY == playerAngY) {
            tempPlayerEmpAngleY += tempPlayerAng;
            PlayerRotationUpdate(tempPlayerEmpAngleY);
        }
    }


    //-------------
    // 回転終了時
    //-------------
    void RotationEnd(float tempPlayerAng, float setTempPlayerAng)
    {
        if (tempPlayerEmpAngleY == tempPlayerAng) {
            RotationDirection = 0;
            playerEmpAngleY = tempPlayerEmpAngleY = setTempPlayerAng;
            RotationFlag = false;
        }
    }


    //---------------------------
    // プレイヤーの回転情報更新
    //---------------------------
    void PlayerRotationUpdate(float rotation)
    {
        m_RotationAngle.y = rotation;

        // PlayerAngleの回転を更新
        m_Rotation = Quaternion.Euler(m_RotationAngle);

        transform.rotation = m_Rotation;
    }
}