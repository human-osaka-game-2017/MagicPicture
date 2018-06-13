using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    
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
    
    private float vertaxis;
    private float horzaxis;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update() {
        
        vertaxis = Input.GetAxis("VerticalForMove");
        horzaxis = Input.GetAxis("HorizontalForMove");
    }

    void FixedUpdate()
    {
        if (GameState.state == ((int)state.play)) {

            Rotation(rotSpeed);

            float verticality = 0;

            if (vertaxis > 0) verticality =  fwdSpeed;   // 前移動
            if (vertaxis < 0) verticality = -backSpeed;  // 後ろ移動

            Vector3 move = (Vector3.forward * verticality + 
                Vector3.right * horzSpeed * horzaxis) * Time.deltaTime;

            // 移動
            transform.Translate(move);
        }
    }
    

    void Rotation(float _rotSpeed)
    {
        // Y軸回転
        transform.Rotate(-Vector3.up * _rotSpeed * 
            Input.GetAxis("HorizontalForView") * Time.deltaTime);
    }
}