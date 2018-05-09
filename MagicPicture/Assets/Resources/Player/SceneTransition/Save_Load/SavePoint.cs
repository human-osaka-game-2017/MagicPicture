using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    
    GameObject      player;
    
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player") {            
            PlayerPrefs.DeleteAll();        // すべてのセーブデータを削除

            SaveElement();
            PlayerPrefs.Save();             // セーブ実行
            
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
        Save("savePosX", player.transform.position.x);
        Save("savePosY", player.transform.position.y);
        Save("savePosZ", player.transform.position.z);
        Save("saveRotX", player.transform.rotation.x);
        Save("saveRotY", player.transform.rotation.y);
        Save("saveRotZ", player.transform.rotation.z);
        Save("saveRotW", player.transform.rotation.w);

        // マジカメなどの情報もOK
    }
}