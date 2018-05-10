using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraSystem: MonoBehaviour {

    [SerializeField] private GameObject FPSCamera;
    [SerializeField] private GameObject TPSCamera;

    private bool isFPSMode = false;
    public bool IsFPSMode
    {
        get { return this.isFPSMode; }
        private set { this.isFPSMode = value; }
    }

    void Start ()
    {

    }

    void Update()
    {
        //FPS/TPSの切り替え
        if (Input.GetKeyDown(KeyCode.R)) ChangeFTMode();
    }

    //FPSモード/TPSモード変更処理
    private void ChangeFTMode()
    {
        FPSCamera.SetActive(!FPSCamera.activeSelf);
        FPSCamera.GetComponent<MagiCame>().Init();
        TPSCamera.SetActive(!TPSCamera.activeSelf);
        isFPSMode = !isFPSMode;
    }
}
