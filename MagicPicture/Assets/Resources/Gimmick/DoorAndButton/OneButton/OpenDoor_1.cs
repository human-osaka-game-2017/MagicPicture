using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_1 : MonoBehaviour
{

    [SerializeField] GameObject doorR_1;
    [SerializeField] GameObject doorL_1;

    public  int   doorState;
    public  float speed;
    public  float openRange;
    public  float closeRange;
    private int   direction;
    private float mixSpeed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (doorState == 0) direction = 0;     // 静止
        if (doorState == 1) direction = 1;     // 開いていく
        if (doorState == 2) direction = -1;     // 閉じていく


        // いろいろ含めた速度
        mixSpeed = speed * direction * Time.deltaTime;

        // ドアを止める
        StopDoor();

        // ドアの開閉
        Movement(Vector3.right * mixSpeed, Vector3.left * mixSpeed);
    }


    //================
    // ドアの移動R_L
    //================
    private void Movement(Vector3 _quantityR, Vector3 _quantityL)
    {
        doorR_1.transform.localPosition += _quantityR;
        doorL_1.transform.localPosition += _quantityL;
    }


    //==========================
    // 2つの要因でドアを止める
    //==========================
    private void StopDoor()
    {
        // ドアが開ききったら止める
        if (doorR_1.transform.localPosition.x >= openRange) {
            doorState = 0;
        }

        // ドアの閉じれる範囲に達したら
        if (doorR_1.transform.localPosition.x < closeRange) {

            Vector3 close = Vector3.zero;

            // ドアの閉じれる範囲とドアのx差分
            close.x = doorR_1.transform.localPosition.x - closeRange;

            // ドアのサイズと可動域に影響せずにできた
            doorR_1.transform.localPosition -= close;
            doorL_1.transform.localPosition += close;

            doorState = 0;
        }
    }


    //=========================
    // もしボタンが押されたら
    //=========================
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Player") {
            if (doorR_1.transform.localPosition.x < openRange) {

                doorState = 1;  // 開いていく
            }
        }
    }


    //=========================
    // もしボタンが離されたら
    //=========================
    private void OnCollisionExit(Collision other)
    {
        doorState = 2;          // 閉じていく

        Movement(Vector3.left / 10, Vector3.right / 10);
    }
}