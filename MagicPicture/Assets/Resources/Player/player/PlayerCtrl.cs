﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public static bool playStopperFlag; 
    
    public int   operationState;

    [SerializeField] private float TPS_FwdSpeed;
    [SerializeField] private float TPS_BackSpeed;
    [SerializeField] private float TPS_HorizontalSpeed;
    [SerializeField] private float TPS_RotSpeed;
    [SerializeField] private float FPS_FwdSpeed;
    [SerializeField] private float FPS_BackSpeed;
    [SerializeField] private float FPS_HorizontalSpeed;
    [SerializeField] private float FPS_RotSpeed;

    public float fwdSpeed
    {
        get
        {
            if (this.GetComponent<CameraSystem>().IsFPSMode) return FPS_FwdSpeed;
            else return TPS_FwdSpeed;
        }
    }

    public float backSpeed
    {
        get
        {
            if (this.GetComponent<CameraSystem>().IsFPSMode) return FPS_BackSpeed;
            else return TPS_BackSpeed;
        }
    }

    public float horzSpeed
    {
        get
        {
            if (this.GetComponent<CameraSystem>().IsFPSMode) return FPS_HorizontalSpeed;
            else return TPS_HorizontalSpeed;
        }
    }

    public float rotSpeed
    {
        get
        {
            if (this.GetComponent<CameraSystem>().IsFPSMode) return FPS_RotSpeed;
            else return TPS_RotSpeed;
        }
    }

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

            Rotation(rotSpeed);
            
            if (vertaxis > 0) verticality = fwdSpeed;   // 前移動
            if (vertaxis < 0) verticality = backSpeed;  // 後ろ移動
            
            // 移動
            move = fwdVec * vertaxis * verticality + rightVec * horzaxis * horzSpeed;

            // 移動
            charctrl.SimpleMove(move);
            
            // 瞬間的に回転するのはモーションブレンドでOK
            //animctrl.SetFloat("Speed", charctrl.velocity.magnitude / Speed);    //追加
        }
    }
    

    private void Rotation(float _rotSpeed)
    {
        // Y軸回転
        transform.Rotate(-Vector3.up * _rotSpeed * 
            Input.GetAxis("HorizontalForView") * Time.deltaTime);
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