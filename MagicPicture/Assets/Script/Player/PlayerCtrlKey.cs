//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCtrlKey : MonoBehaviour {

//    [SerializeField] PlayerCtrl playerCtrl;

//    private CharacterController charctrl;
//    private float vertaxis;
//    private float horzaxis;
//    private float addRotSpeed;

//    // Use this for initialization
//    void Start () {
//        charctrl = GetComponent<CharacterController>();
//    }
	
//	// Update is called once per frame
//	void Update () {

//        vertaxis = horzaxis = 0;

//        // キーボード
//        if (playerCtrl.operationState == 0) {

//            if (Input.GetKey("w")) {
//                vertaxis = 1;
//            }
//            if (Input.GetKey("s")) {
//                vertaxis = -1;
//            }

//            // FPS時
//            if (this.GetComponent<CameraSystem>().IsFPSMode) {

//                if (Input.GetKey("d")) {
//                    horzaxis = 1;
//                }
//                if (Input.GetKey("a")) {
//                    horzaxis = -1;
//                }
//            }
//        }
//    }

    
//    void FixedUpdate()
//    {
//        Vector3 move        = Vector3.zero;
//        Vector3 rotation    = Vector3.zero;
//        Vector3 fwdVec      = Vector3.zero;
//        Vector3 rightVec    = Vector3.zero;
//        float   verticality = 0;

//        if (!PlayerCtrl.GetStopperFlag()) {

//            fwdVec   = transform.forward;
//            rightVec = transform.right;

//            if (!this.GetComponent<CameraSystem>().IsFPSMode) {

//                Rotation(playerCtrl.TPS_RotSpeed);
//            }
//            // FPS時
//            if (this.GetComponent<CameraSystem>().IsFPSMode) {

//                Rotation(playerCtrl.FPS_RotSpeed);
//            }

//            if (vertaxis > 0) verticality = playerCtrl.fwdSpeed;   // 前移動
//            if (vertaxis < 0) verticality = playerCtrl.backSpeed;  // 後ろ移動

//            // 移動
//            move = fwdVec * vertaxis * verticality + rightVec * horzaxis * playerCtrl.horizontalSpeed;

//            // 移動
//            charctrl.SimpleMove(move);
//        }
//    }

//    void Rotation(float _rotSpeed)
//    {
//        if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) {
//            addRotSpeed = 0;
//        }

//        if (Input.GetKey("right")) {
//            if (addRotSpeed < 0.5f) addRotSpeed += 0.005f;

//            transform.Rotate(0, _rotSpeed * addRotSpeed, 0);
//        }
//        if (Input.GetKey("left")) {
//            if (addRotSpeed < 0.5f) addRotSpeed += 0.005f;

//            transform.Rotate(0, -_rotSpeed * addRotSpeed, 0);
//        }
//    }
//}