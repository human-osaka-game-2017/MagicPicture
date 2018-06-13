using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingData : MonoBehaviour {
    
    private Vector3    loadPos;
    private Quaternion loadRot;

    // Use this for initialization
    void Start () {
        
        if (GameState.GetState() == (int)state.load) {
            
            // ロードする要素を呼び出し
            LoadElement();
            
            transform.position = loadPos;
            transform.rotation = loadRot;

            GameState.state = (int)state.play;
        }
    }
	
	// Update is called once per frame
	void Update () {

    }


    //-----------------------
    // 要素をロードする関数
    float Load(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName, -1);
    }
    
    //-------------------
    // ロード要素を列挙
    void LoadElement()
    {
        loadPos.x = Load("savePosX");
        loadPos.y = Load("savePosY");
        loadPos.z = Load("savePosZ");
        loadRot.x = Load("saveRotX");
        loadRot.y = Load("saveRotY");
        loadRot.z = Load("saveRotZ");
        loadRot.w = Load("saveRotW");
    }
}