using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    public static bool  hitFlag = false;
    public static int   directionMove = 0;      // 動く方向
    private const int   m_SpeedSlow = 4;        // 跳ね返り速度減速

    float       animSpeed = 120f / 1000f;
    // float AnimSpeed = 120f / 1000f;
    private float       m_NextPosition = 2;    // 次の目的地分の距離
    // private float x = 0, y = 0, z = 0;
    private int         OppositeMove;           // 反対方向
    private int         speedDiv = 1;           // 衝突モーションが収まるようにanimSpeedを分割
    private Vector3 pos;                        // キャラのポジション
    private Vector3 comparisonPos;              // 距離を比較するベクトル

    // Use this for initialization
    void Start()
    {
        Debug.Log("Hello World");        
        comparisonPos = pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos;

        if (hitFlag &&
            directionMove == 1 || directionMove == 2) IfHit(comparisonPos.z);
        if (hitFlag &&
            directionMove == 3 || directionMove == 4) IfHit(comparisonPos.x);

        if (directionMove == 0) MoveRot();
        if (directionMove != 0) Move();
    }

    
    //-------------
    // 向かう場所
    void MoveRot()
    {
        if (Input.GetKeyDown("up")) {
            directionMove = 1;
            OppositeMove = 2;
            comparisonPos.z += m_NextPosition;
        }
        if (Input.GetKeyDown("down")) {
            directionMove = 2;
            OppositeMove = 1;
            comparisonPos.z -= m_NextPosition;
        }
        if (Input.GetKeyDown("right")) {
            directionMove = 3;
            OppositeMove = 4;
            comparisonPos.x += m_NextPosition;
        }
        if (Input.GetKeyDown("left")) {
            directionMove = 4;
            OppositeMove = 3;
            comparisonPos.x -= m_NextPosition;
        }

        GameObject Stairs = GameObject.Find("Stairs");

        if (transform.position.y - 1 < Stairs.transform.position.y + 0.5) {
            // moveKeyNum = 9;
           // Debug.Log("OK!");
            //transform.position += new Vector3(0, 0.02f, 0);
        }
    }


    //-------
    // 動き
    void Move()
    {
        if (directionMove == 1) {
            // if (transform.position.z <= pos.z) {
            if (pos.z <= comparisonPos.z) {
                //transform.position += new Vector3(0, 0, animSpeed / speedDiv);  // アニメーション速度と合わせる
                pos.z += animSpeed / speedDiv;
                Debug.Log("AAA");
            }
        }
        if (directionMove == 2) {
            if (pos.z >= comparisonPos.z) {
                //transform.position -= new Vector3(0, 0, animSpeed / speedDiv);  // アニメーション速度と合わせる
                pos.z -= animSpeed / speedDiv;
                Debug.Log("BBB");
            }
        }
        if (directionMove == 3) {
            if (pos.x <= comparisonPos.x) {
                //transform.position += new Vector3(animSpeed / speedDiv, 0, 0);  // アニメーション速度と合わせる
                pos.x += animSpeed / speedDiv;
            }
        }
        if (directionMove == 4) {
            if (pos.x >= comparisonPos.x) {
                //transform.position -= new Vector3(animSpeed / speedDiv, 0, 0);  // アニメーション速度と合わせる
                pos.x -= animSpeed / speedDiv;
            }
        }
        /*if (directionMove == 9) {

        }*/

        
        // 完璧なるマス移動Z
        if (directionMove == 1 || directionMove == 2) {
            if (pos.z >= (comparisonPos.z - 0.5f) && // animSpeed / speedDiv
                pos.z <= (comparisonPos.z + 0.5f)) {
                //float px = this.transform.position.x;
                //float py = this.transform.position.y;
                //transform.position = new Vector3(px, py, comparisonPos.z);
                pos.z = comparisonPos.z;
                directionMove = 0; speedDiv = 1;
            }
        }
        // 完璧なるマス移動X
        if (directionMove == 3 || directionMove == 4) {
            if (pos.x >= (comparisonPos.x - 0.5f) &&
                pos.x <= (comparisonPos.x + 0.5f)) {
                //float py = this.transform.position.y;
                //float pz = this.transform.position.z;
                //transform.position = new Vector3(comparisonPos.x, py, pz);
                pos.x = comparisonPos.x;
                directionMove = 0; speedDiv = 1;
            }
        }
    }


    //---------------------------------------
    // 壁にぶつかったときのキャラクター処理
    // (とっさに反対を向き、進む前の位置に戻る)
    void IfHit(float Coordinate)
    {
        if (directionMove < OppositeMove) {
            Coordinate -= m_NextPosition;
            directionMove = OppositeMove;
            speedDiv = m_SpeedSlow;
        }
        if (directionMove > OppositeMove) {
            Coordinate += m_NextPosition;
            directionMove = OppositeMove;
            speedDiv = m_SpeedSlow;
        }

        /*if (directionMove == 1) {
            Coordinate -= m_NextPosition;
            directionMove = 2;
            speedDiv = m_SpeedSlow;
        }*/

        hitFlag = false;
    }

    /*if (moveKeyNum == 5) {
            z -= distancePoint;
            moveKeyNum = 2;
            speedDiv = m_SpeedSlow;
        }
        if (moveKeyNum == 6) {
            z += distancePoint;
            moveKeyNum = 1;
            speedDiv = m_SpeedSlow;
        }
        if (moveKeyNum == 7) {
            x -= distancePoint;
            moveKeyNum = 4;
            speedDiv = m_SpeedSlow;
        }
        if (moveKeyNum == 8) {
            x += distancePoint;
            moveKeyNum = 3;
            speedDiv = m_SpeedSlow;
        }*/
}