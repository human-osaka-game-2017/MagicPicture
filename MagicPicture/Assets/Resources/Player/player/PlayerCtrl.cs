using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    
    public static bool playStopperFlag;   
    
    public float fwdSpeed;
    public float backSpeed;
    public float horizontalSpeed;
    public float TPS_RotSpeed;
    public float FPS_RotSpeed;

    private CharacterController charctrl;
    private float vertaxis;
    private float horzaxis;
    private float addRotSpeed;

    // Use this for initialization
    void Start () {
        playStopperFlag = false;

        charctrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        
        vertaxis = Input.GetAxis("VerticalForMove");
        horzaxis = Input.GetAxis("HorizontalForMove");
    }

    void FixedUpdate()
    {
        Vector3 move        = Vector3.zero;
        Vector3 rotation    = Vector3.zero;
        Vector3 fwdVec      = Vector3.zero;
        Vector3 rightVec    = Vector3.zero;
        float   verticality = 0;

        if (!playStopperFlag) {

            fwdVec   = transform.forward;
            rightVec = transform.right;

            if (!this.GetComponent<CameraSystem>().IsFPSMode) {

                Rotation(TPS_RotSpeed);
            }
            // FPS時
            if (this.GetComponent<CameraSystem>().IsFPSMode) {

                Rotation(FPS_RotSpeed);
            }
            
            if (vertaxis > 0) verticality = fwdSpeed;   // 前移動
            if (vertaxis < 0) verticality = backSpeed;  // 後ろ移動
            
            // 移動
            move = fwdVec * vertaxis * verticality + rightVec * horzaxis * horizontalSpeed;

            // 移動
            charctrl.SimpleMove(move);
            
            // 瞬間的に回転するのはモーションブレンドでOK
            //animctrl.SetFloat("Speed", charctrl.velocity.magnitude / Speed);    //追加
        }
    }
    

    private void Rotation(float _rotSpeed)
    {
        // Y軸回転
        if (Input.GetAxis("HorizontalForView") != 0) {
            if (addRotSpeed < 0.5f) {
                addRotSpeed += 0.005f;
            }
        }
        if (Input.GetAxis("HorizontalForView") == 0) {
            addRotSpeed = 0;
        }
        if (Input.GetAxis("HorizontalForView") > 0) {
            transform.Rotate(0, -_rotSpeed * addRotSpeed, 0);
        }
        if (Input.GetAxis("HorizontalForView") < 0) {
            transform.Rotate(0, _rotSpeed * addRotSpeed, 0);
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
}