using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scene {
    e_Start,    // 0 = スタート時
    e_NewGame,  // 1 = 初めから
    e_LoadGame, // 2 = 続きから
    e_Gameing,  // 3 = ゲーム進行中
    e_GameOver  // 4 = ゲームオーバー遷移
}

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    TitleScene titleScene;

    public static int   sceneDivergence;
    public Material[]   _material;

    const float         playerSpeed = 0.05f;
    private int         loadCount;
    
    // Use this for initialization
    void Start () {
<<<<<<< HEAD
        TitleScene meteortmp = Instantiate(titleScene);
        meteortmp.gameObject.SetActive(true);
=======
>>>>>>> 2f413c416e5daff3c06b2a9b1012b1e78622001c
    }

<<<<<<< HEAD
    // Update is called once per frame
    void Update()
    {
        if (sceneDivergence == (int)Scene.e_Start) {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            GetComponent<Renderer>().material = _material[0];
        }

        if (sceneDivergence == (int)Scene.e_LoadGame) {
            ExecuteLoad();
            sceneDivergence = (int)Scene.e_Gameing;
=======
        // TPS時
        if (!this.GetComponent<CameraSystem>().IsFPSMode) {
            TPSMove();
        }

        // FPS時
        if (this.GetComponent<CameraSystem>().IsFPSMode) {
            FPSMove();
>>>>>>> 2f413c416e5daff3c06b2a9b1012b1e78622001c
        }
    }
    
    void FixedUpdate()
    {
<<<<<<< HEAD
        if (sceneDivergence == (int)Scene.e_Gameing) {

            // TPS時
            if (!fCameraSystem.changeMode) {
                ForwardAndBack();
            }

            // FPS時
            if (fCameraSystem.changeMode) {
                ForwardAndBack();
                RightAndLeft();
            }
=======
        if (changeVector != 0) {
            transform.position += transform.up * playerSpeed * changeVector;
            Camera.main.transform.Translate(transform.up * playerSpeed * changeVector);
>>>>>>> 2f413c416e5daff3c06b2a9b1012b1e78622001c
        }
    }


    //===========
    // 前後移動
    //===========
    void ForwardAndBack()
    {
        if (Input.GetKey("w")) {
            transform.position += transform.forward * playerSpeed;
        }
        if (Input.GetKey("s")) {
            transform.position -= transform.forward * playerSpeed;
        }
    }


    //===========
    // 左右移動
    //===========
    void RightAndLeft()
    {
<<<<<<< HEAD
=======
        Vector3 movement = Vector3.zero;

        if (Input.GetKey("w")) {
            movement -= transform.up * playerSpeed;
        }
        if (Input.GetKey("s")) {
            movement += transform.up * playerSpeed;
        }
>>>>>>> 2f413c416e5daff3c06b2a9b1012b1e78622001c
        if (Input.GetKey("a")) {
            movement -= transform.right * playerSpeed;
        }
        if (Input.GetKey("d")) {
            movement += transform.right * playerSpeed;
        }

        this.transform.Translate(movement);
        Camera.main.transform.Translate(movement);
    }


    //=======================
    // 要素をロードする関数
    //=======================
    float Load(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName, -1);
    }


    //=============
    // ロード実行
    //=============
    void ExecuteLoad()
    {
        LoadElement();
    }


    //===================
    // ロード要素を列挙
    //===================
    void LoadElement()
    {
        Vector3     loadPos;
        Quaternion  loadRot;

        loadPos.x = Load("savePosX");
        loadPos.y = Load("savePosY");
        loadPos.z = Load("savePosZ");
        loadRot.x = Load("saveRotX");
        loadRot.y = Load("saveRotY");
        loadRot.z = Load("saveRotZ");
        loadRot.w = Load("saveRotW");

        transform.position = loadPos;
        transform.rotation = loadRot;
    }
}