using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    [SerializeField] GameObject TPSCameraEmpty;

    public static bool playStopperFlag;   
    
    public float speed;
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
        
        vertaxis = Input.GetAxis("Vertical");
        horzaxis = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Vector3 move        = Vector3.zero;
        Vector3 rotation    = Vector3.zero;
        Vector3 fwdVec   = Vector3.zero;
        Vector3 rightVec = Vector3.zero;
        
        if (!playStopperFlag) {

            if (!this.GetComponent<CameraSystem>().IsFPSMode) {

                fwdVec   = TPSCameraEmpty.transform.forward;
                rightVec = TPSCameraEmpty.transform.right;
            }
            // FPS時
            if (this.GetComponent<CameraSystem>().IsFPSMode) {

                fwdVec   = transform.forward;
                rightVec = transform.right;

                FPSRotation();
            }
            
            // 移動方向
            move = fwdVec * vertaxis + rightVec * horzaxis;

            // 移動
            charctrl.SimpleMove(move * speed);
            
            // もし移動中なら
            if (move.magnitude > 0) {

                if (!this.GetComponent<CameraSystem>().IsFPSMode) {
                    
                    rotation.y = Vector3.SignedAngle(Vector3.forward, move, Vector3.up);                    
                    transform.eulerAngles = rotation;
                }

                // 瞬間的に回転するのはモーションブレンドでOK
                //animctrl.SetFloat("Speed", charctrl.velocity.magnitude / Speed);    //追加
            }
        }
    }
    

    private void FPSRotation()
    {
        // Y軸回転
        if (Input.GetAxis("Horizontal2") != 0) {
            if (addRotSpeed < 0.5f) addRotSpeed += 0.005f;
        }
        if (Input.GetAxis("Horizontal2") == 0) {
            addRotSpeed = 0;
        }
        if (Input.GetAxis("Horizontal2") > 0) {
            transform.Rotate(0, -FPS_RotSpeed * addRotSpeed, 0);
        }
        if (Input.GetAxis("Horizontal2") < 0) {
            transform.Rotate(0, FPS_RotSpeed * addRotSpeed, 0);
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