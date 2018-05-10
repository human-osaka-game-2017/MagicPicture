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
        if (!this.GetComponent<CameraSystem>().IsFPSMode) {
            TPSMove();
        }

        // FPS時
        if (this.GetComponent<CameraSystem>().IsFPSMode) {
            FPSMove();
        }
    }


    void FixedUpdate()
    {
        if (changeVector != 0) {
            transform.position += transform.forward * playerSpeed * changeVector;
            //Camera.main.transform.Translate(transform.forward * playerSpeed * changeVector);
        }
    }


    void TPSMove()
    {
        if (Input.GetKey("w")) {
            changeVector = 1;
        }
        if (Input.GetKey("s")) {
            changeVector = -1;
        }
    }


    void FPSMove()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey("w")) {
            movement += transform.forward * playerSpeed;
        }
        if (Input.GetKey("s")) {
            movement -= transform.forward * playerSpeed;
        }
        if (Input.GetKey("a")) {
            movement = transform.right * playerSpeed;
        }
        if (Input.GetKey("d")) {
            movement += transform.right * playerSpeed;
        }

        this.transform.Translate(movement);
    }
}