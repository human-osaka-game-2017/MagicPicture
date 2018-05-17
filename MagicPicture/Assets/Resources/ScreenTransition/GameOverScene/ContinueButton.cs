using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {

    [SerializeField]
    GameOverScene       GameOverScene;

    [SerializeField]
    PlayerLoadingData   PlayerLoad;

    [SerializeField]
    PlayerMove          PlayerMove;

    private Vector3     PlayerLoadPos;
    private Quaternion  PlayerLoadRot;
    private Button      button;

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {
        LoadElement();
    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        /*// 落下ゲームオーバー時に落下を止めるための処理(Freeze)を無効化と再設定
        PlayerMove.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        PlayerMove.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        CombinationOnButton1.SetButtonNum(0);
        CombinationOnButton2.SetButtonNum(0);
        CombinationOnButton3.SetButtonNum(0);*/

        ResetElements();

        // プレイヤー情報をロード(position, rotation)
        PlayerLoad.transform.position = PlayerLoadPos;
        PlayerLoad.transform.rotation = PlayerLoadRot;

        // 静的変数の再設定
        PlayerMove.Reset();

        // GameOverSceneを非アクティブ化
        GameOverScene.gameObject.SetActive(false);
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


    //=======================
    // 様々な要素をリセット
    //=======================
    void ResetElements()
    {
        CombOnButton1.SetOnFlag(false);
        CombOnButton2.SetOnFlag(false);
        CombOnButton3.SetOnFlag(false);

        // 落下ゲームオーバー時に落下を止めるための処理(Freeze)を無効化と再設定
        PlayerMove.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        PlayerMove.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}