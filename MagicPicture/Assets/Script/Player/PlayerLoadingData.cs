using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingData : MonoBehaviour {
    
    private Vector3             PlayerLoadPos;
    private Quaternion          PlayerLoadRot;
    private CharacterController charctrl;

    // Use this for initialization
    void Start () {

        charctrl = GetComponent<CharacterController>();

        if (GameState.GetGameState() == (int)state.load) {

            // ロードする要素を呼び出し
            LoadElement();

            charctrl.transform.position = PlayerLoadPos;
            charctrl.transform.rotation = PlayerLoadRot;

            PlayerCtrl.playStopperFlag = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

    }


    //=======================
    // 要素をロードする関数
    //=======================
    private float Load(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName, -1);
    }


    //===================
    // ロード要素を列挙
    //===================
    private void LoadElement()
    {
        PlayerLoadPos.x = Load("savePosX");
        PlayerLoadPos.y = Load("savePosY");
        PlayerLoadPos.z = Load("savePosZ");
        PlayerLoadRot.x = Load("saveRotX");
        PlayerLoadRot.y = Load("saveRotY");
        PlayerLoadRot.z = Load("saveRotZ");
        PlayerLoadRot.w = Load("saveRotW");
    }
}