using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject FPSCamera;
    [SerializeField] private GameObject TPSCamera;

    private bool isFPSMode = false;
    public bool IsFPSMode
    {
        get { return this.isFPSMode; }
        private set { this.isFPSMode = value; }
    }

    void Start () {
		
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z)) ChangeFTMode();
    }

    //FPSモード/TPSモード変更処理
    void ChangeFTMode()
    {

        FPSCamera.SetActive(!FPSCamera.activeSelf);
        TPSCamera.SetActive(!TPSCamera.activeSelf);
        isFPSMode = !isFPSMode;
    }
}
