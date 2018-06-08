using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    
    private GameObject Player;
    
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        
	}


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player") {
            SoundManager.GetInstance().Play("SE_IntermediatePoint", SoundManager.PLAYER_TYPE.NONLOOP, true);

            PlayerPrefs.DeleteAll();    // すべてのセーブデータを削除

            SaveElement();
            PlayerPrefs.Save();         // セーブ実行

            Destroy(gameObject);
        }
    }
    

    //=================
    // セーブする関数
    //=================
    void Save(string keyName, float fSave)
    {
        PlayerPrefs.SetFloat(keyName, fSave);
    }


    //===================
    // セーブ要素を列挙
    //===================
    void SaveElement()
    {
        Save("savePosX", Player.transform.position.x);
        Save("savePosY", Player.transform.position.y);
        Save("savePosZ", Player.transform.position.z);
        Save("saveRotX", Player.transform.rotation.x);
        Save("saveRotY", Player.transform.rotation.y);
        Save("saveRotZ", Player.transform.rotation.z);
        Save("saveRotW", Player.transform.rotation.w);

        // マジカメなどの情報もOK
    }
}