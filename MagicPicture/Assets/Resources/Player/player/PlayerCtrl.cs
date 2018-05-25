using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    
    public static bool playStopperFlag;
    
    public float speed = 5;      // デフォルト値
    
    private CharacterController charctrl;
    private float vertaxis;
    private float horzaxis;

    // Use this for initialization
    void Start () {
        playStopperFlag = false;

        charctrl = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        /*if (!this.GetComponent<CameraSystem>().IsFPSMode) {
            
        }
        // FPS時
        if (this.GetComponent<CameraSystem>().IsFPSMode) {

        }*/

        vertaxis = Input.GetAxis("Vertical");
        horzaxis = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Vector3 move     = Vector3.zero;
        Vector3 rotation = Vector3.zero;

        if (!playStopperFlag) {

            // 移動方向
            move = Vector3.forward * vertaxis + Vector3.right * horzaxis;

            // 移動
            charctrl.SimpleMove(move * speed);
            
            // もし移動中なら
            if (move.magnitude > 0) {

                // 移動方向に向く
                rotation.y = Vector3.SignedAngle(Vector3.forward, move, Vector3.up);

                // 回転
                transform.eulerAngles = rotation;

                // 瞬間的に回転するのはモーションブレンドでOK
                //animctrl.SetFloat("Speed", charctrl.velocity.magnitude / Speed);    //追加
            }
        }

    }


    public static bool GetStopperFlag()
    {
        return playStopperFlag;
    }

    public static void SetStopperFlag(bool _flag)
    {
        playStopperFlag = _flag;
    }


    //===================
    // 静的変数の再設定
    //===================
    public static void Reset()
    {
        playStopperFlag = false;
    }
}