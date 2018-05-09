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
        TitleScene meteortmp = Instantiate(titleScene);
        meteortmp.gameObject.SetActive(true);
    }

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
        }
    }
    
    void FixedUpdate()
    {
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
        if (Input.GetKey("a")) {
            transform.position -= transform.right * playerSpeed;
        }
        if (Input.GetKey("d")) {
            transform.position += transform.right * playerSpeed;
        }
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