using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float        playerSpeed = 0.05f;    // デフォルト = 0.05f
    private float       changeDirectionX;
    private float       changeDirectionZ;
    public static bool  playStopperFlag;
    private Rigidbody   RigidbodyComponent;
    
    // Use this for initialization
    void Start () {
        RigidbodyComponent = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update () {
        
        if (playStopperFlag) RigidbodyComponent.constraints = RigidbodyConstraints.FreezeAll;

        if (!playStopperFlag)

        if (!this.GetComponent<CameraSystem>().IsFPSMode) {
            DirectionZ();
        }

        // FPS時
        if (this.GetComponent<CameraSystem>().IsFPSMode) {
            DirectionX();
            DirectionZ();
        }
    }
    
    void FixedUpdate()
    {
        if (changeDirectionX != 0) {
            transform.position += transform.right * playerSpeed * changeDirectionX;
        }
        if (changeDirectionZ != 0) {
            transform.position += transform.forward * playerSpeed * changeDirectionZ;
        }

        changeDirectionX = 0;
        changeDirectionZ = 0;
    }
    

    void DirectionZ()
    {
        if (Input.GetKey("w")) {
            changeDirectionZ = 1;
        }
        if (Input.GetKey("s")) {
            changeDirectionZ = -1;
        }
    }


    void DirectionX()
    {
        if (Input.GetKey("a")) {
            changeDirectionX = -1;
        }
        if (Input.GetKey("d")) {
            changeDirectionX = 1;
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