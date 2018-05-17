using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoLoadGamingScene : MonoBehaviour {
    
    public static Vector3       PlayerLoadPos;
    public static Quaternion    PlayerLoadRot;
    private Button              button;
    
    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {
        
    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        // ロードしてゲームに入るためのフラグ
        NewOrLoad.SetLoadFlag(true);

        // プレイヤー情報をロード(position, rotation)
        LoadElement();

        // 静的変数の再設定
        PlayerMove.Reset();

        // ゲームのシーンを開く
        SceneManager.LoadScene("Funaoka");
    }


    //=======================
    // 要素をロードする関数
    //=======================
    float Load(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName, -1);
    }


    //===================
    // ロード要素を列挙
    //===================
    void LoadElement()
    {
        PlayerLoadPos.x = Load("savePosX");
        PlayerLoadPos.y = Load("savePosY");
        PlayerLoadPos.z = Load("savePosZ");
        PlayerLoadRot.x = Load("saveRotX");
        PlayerLoadRot.y = Load("saveRotY");
        PlayerLoadRot.z = Load("saveRotZ");
        PlayerLoadRot.w = Load("saveRotW");
    }


    //======================================
    // 別スクリプトにPlayerPosをロードする
    //======================================
    public static Vector3 GetLoadPos()
    {
        return PlayerLoadPos;
    }


    //======================================
    // 別スクリプトにPlayerRotをロードする
    //======================================
    public static Quaternion GetLoadRot()
    {
        return PlayerLoadRot;
    }
}