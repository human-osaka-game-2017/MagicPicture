using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Vector3     m_Pos;
    private Vector3     m_NextPos;
    private Vector3     m_BackPos;

    // 移動している状態
    public static bool  gameOverFlag;
    public static bool  isMoveng;
    

    // Use this for initialization
    void Start() {
        isMoveng = false;
        gameOverFlag = false;

        m_Pos = m_NextPos = m_BackPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        // ゲームオーバーじゃなかったら
        if (gameOverFlag == false) {

            // 回転中じゃなかったら
            if (PlayerRotation.RotationFlag == false) {
                if (m_Pos == m_NextPos) {
                    SetPlayerPosition();
                }
                if (m_Pos != m_NextPos) {
                    GoNextPosition();
                    JustPosition();
                }
            }
        }
        
        PlayerPositionUpdate();
        FallGameOver();
        EnemyGameOver();
    }

    
    //=====================
    // プレイヤー情報更新
    //---------------------------
    // プレイヤーの位置情報更新
    void PlayerPositionUpdate()
    {
        // プレイヤー位置を更新(落下時無効)
        if (HitJudgment.HitFloorFlag == true) {

            int pos_y = (int)m_Pos.y;                       // Y整数化

            m_Pos.y = m_NextPos.y = m_BackPos.y = pos_y;    // Y整数値代入

            transform.position = m_Pos;
        }

        // 重力で移動した分のYを代入(落下時のみ有効)
        if (HitJudgment.HitFloorFlag == false) {
            m_Pos.y = m_NextPos.y = m_BackPos.y = transform.position.y;
        }
    }

    
    //===================
    // ゲームオーバー時
    //-----------------------------
    // 落下ゲームオーバー時の処理
    void FallGameOver()
    {
        if (HitJudgment.HitFallGameOverPlaneFlag) {
            // 画面遷移しよう
            Debug.Log("GameOver!");

            HitJudgment.HitFallGameOverPlaneFlag = false;

            gameOverFlag = true;
        }
    }

    //---------------------------
    // 敵ゲームオーバー時の処理
    //---------------------------
    void EnemyGameOver()
    {
        if (HitJudgment.HitEnemyFlag) {
            // 画面遷移しよう
            Debug.Log("GameOver!");

            HitJudgment.HitEnemyFlag = false;

            gameOverFlag = true;
        }
    }


    //-----------------------------------------------
    // プレイヤーの平行移動値代入(壁あたり判定あり)
    //-----------------------------------------------
    void SetPlayerPosition()
    {
        if (Input.GetKeyDown("w") && FrontHit.FrontHitFlag == false) {
            m_NextPos.z += 1;
        }
        else if (Input.GetKeyDown("s") && BackHit.BackHitFlag == false) {
            m_NextPos.z -= 1;
        }
        else if (Input.GetKeyDown("d") && RightHit.RightHitFlag == false) {
            m_NextPos.x += 1;
        }
        else if (Input.GetKeyDown("a") && LeftHit.LeftHitFlag == false) {            
            m_NextPos.x -= 1;
        }
    }


    //-----------------------
    // プレイヤーの平行移動
    //-----------------------
    void GoNextPosition()
    {
        if (isMoveng == false) isMoveng = true;

        if (m_NextPos.z >= m_Pos.z) {
            m_Pos.z += 0.05f;
        }
        if (m_NextPos.z <= m_Pos.z) {
            m_Pos.z -= 0.05f;
        }
        if (m_NextPos.x >= m_Pos.x) {
            m_Pos.x += 0.05f;
        }
        if (m_NextPos.x <= m_Pos.x) {
            m_Pos.x -= 0.05f;
        }
    }

    //-------------------
    // 完璧なる位置揃え
    //-------------------
    void JustPosition()
    {
        float DifferenceZ = m_NextPos.z - m_Pos.z;
        float DifferenceX = m_NextPos.x - m_Pos.x;

        if (DifferenceZ < 0.05f && DifferenceZ > -0.05f) {
            m_Pos.z = m_NextPos.z;
        }
        if (DifferenceX < 0.05f && DifferenceX > -0.05f) {
            m_Pos.x = m_NextPos.x;
        }

        if (m_Pos == m_NextPos) {
            isMoveng = false;
            m_BackPos = m_Pos;
        }
    }
}