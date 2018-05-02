using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Vector3             pos;
    Vector3             pos2;
    Vector3             falseGravity;
    Rigidbody           RigidBody;
    float               fallDistanceY;    
    public static float strengthDrag;


    // Use this for initialization
    void Start () {
        RigidBody = GetComponent<Rigidbody>();

        fallDistanceY = transform.position.y;

        falseGravity.y = -0.1f;
    }
    
    // Update is called once per frame
    void Update () {
        
        // TPS時
        if (!CameraSystem2.changeMode) {
            TPSMove();
        }

        // FPS時
        if (CameraSystem2.changeMode) {
            FPSMove();
        }

        // 回転時
        if (PlayerRotation.rotationFlag) {
            strengthDrag = 3.5f;
        }

        // 重力処理
        GravitySystem();
        
        
        RigidBody.drag = strengthDrag;
        RigidBody.AddForce(falseGravity);
        RigidBody.AddForce(-transform.up * pos.z);
        RigidBody.AddForce(-transform.right * pos.x);        
    }


    void TPSMove()
    {
        if (Input.GetKey("w")) {
            pos.z = 2;
        }
        if (Input.GetKey("s")) {
            pos.z = -2;
        }

        if (Input.GetKey("w") || Input.GetKey("s")) {
            strengthDrag = 0;
        }
        if (!Input.GetKey("w") && !Input.GetKey("s")) {
            pos.z = 0;
            strengthDrag = 5;
        }
    }


    void FPSMove()
    {
        if (Input.GetKey("w")) {
            pos.z = 2;
        }
        if (Input.GetKey("s")) {
            pos.z = -2;
        }
        if (Input.GetKey("a")) {
            pos.x = 2;
        }
        if (Input.GetKey("d")) {
            pos.x = -2;
        }
        
        if (!Input.GetKey("w") && !Input.GetKey("s")) {
            pos.z = 0;
            strengthDrag = 5;
        }
        if (!Input.GetKey("a") && !Input.GetKey("d")) {
            pos.x = 0;
            strengthDrag = 5;
        }
        
        if (pos.z != 0 || pos.z != 0) {
            strengthDrag = 0;
        }
    }


    void GravitySystem()
    {
        if (transform.position.y < fallDistanceY) {
            RigidBody.useGravity = true;
        }
        if (transform.position.y == fallDistanceY) {
            RigidBody.useGravity = false;
        }
        if (RigidBody.useGravity) {
            strengthDrag = 0;
            fallDistanceY = transform.position.y;
        }
    }
}