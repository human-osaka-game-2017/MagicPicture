using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_1 : MonoBehaviour {

    [SerializeField] DoorR_1 doorR_1;
    [SerializeField] DoorL_1 doorL_1;

    public float    speed = 0.5f;   // デフォルト値

    public int      doorState;
    private int     direction;
    
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {

        if (doorState == 0) direction =  0;     // 静止
        if (doorState == 1) direction =  1;     // 開いていく
        if (doorState == 2) direction = -1;     // 閉じていく

        // ドアが開ききったら止める
        if (doorR_1.transform.localPosition.x >= 2) {
            doorState = 0;
        }
        
        // ボタンが押されてない時は閉まる
        if (doorR_1.transform.localPosition.x < 0.75f) {
            doorR_1.transform.localPosition = new Vector3(0.75f, 1.5f, 3);
            doorL_1.transform.localPosition = new Vector3(-0.75f, 1.5f, 3);
            doorState = 0;
        }

        // ドアの開閉
        doorR_1.transform.Translate(Vector3.right * direction * Time.deltaTime);
        doorL_1.transform.Translate(Vector3.left *  direction * Time.deltaTime);
    }


    //=========================
    // もしボタンが押されたら
    //=========================
    private void OnCollisionEnter(Collision other)
    {
        if (doorR_1.transform.localPosition.x < 2) {
            doorState = 1;      // 開いていく
        }
    }

    //=========================
    // もしボタンが離されたら
    //=========================
    private void OnCollisionExit(Collision other)
    {
        doorState = 2;          // 閉じていく

        doorR_1.transform.localPosition -= new Vector3(0.1f, 0, 0);
        doorL_1.transform.localPosition += new Vector3(0.1f, 0, 0);
        //doorL_1.transform.Translate(Vector3.left * direction * Time.deltaTime);
    }
}