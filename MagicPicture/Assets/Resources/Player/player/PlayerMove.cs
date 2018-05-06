using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    float           changeVector;
    const float     playerSpeed = 0.05f;


    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {

        changeVector = 0;

        // TPS時
        if (!fCameraSystem.changeMode) {
            TPSMove();
        }

        // FPS時
        if (fCameraSystem.changeMode) {
            FPSMove();
        }
    }


    void FixedUpdate()
    {
        if (changeVector != 0) {
            transform.position += transform.up * playerSpeed * changeVector;
        }
    }


    void TPSMove()
    {
        if (Input.GetKey("w")) {
            changeVector = -1;
        }
        if (Input.GetKey("s")) {
            changeVector = 1;
        }
    }


    void FPSMove()
    {
        if (Input.GetKey("w")) {
            transform.position -= transform.up * playerSpeed;
        }
        if (Input.GetKey("s")) {
            transform.position += transform.up * playerSpeed;
        }
        if (Input.GetKey("a")) {
            transform.position -= transform.right * playerSpeed;
        }
        if (Input.GetKey("d")) {
            transform.position += transform.right * playerSpeed;
        }
    }
}