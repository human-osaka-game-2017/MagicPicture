using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    
    [SerializeField] GameObject player;
    private string     myName;
    private bool       startOKFlag;
    private float      startTimer;
    

    // Use this for initialization
    void Start () {
        // saveのパスを表示
        Debug.Log(Application.persistentDataPath);
    }
	
	// Update is called once per frame
	void Update () {
        if (startTimer < 1.0f) {
            startTimer += Time.deltaTime;
        }
        else startOKFlag = true;
    }


    void OnTriggerEnter(Collider col)
    {
        // playになった瞬間は効果音停止
        
        if (col.gameObject.name == player.name) {

            if (startOKFlag) {
                SoundManager.GetInstance().Play("SE_IntermediatePoint", SoundManager.PLAYER_TYPE.NONLOOP, true);
            }

            myName = gameObject.name;

            SaveElement();
            PlayerPrefs.Save();         // セーブ実行

            Destroy(gameObject);
        }
    }
    

    //=================
    // セーブする関数
    //=================
    void SaveFloat(string keyName, float fSave)
    {
        PlayerPrefs.SetFloat(keyName, fSave);
    }

    void SaveString(string keyName, string sSave)
    {
        PlayerPrefs.SetString(keyName, sSave);
    }


    //===================
    // セーブ要素を列挙
    //===================
    void SaveElement()
    {
        SaveFloat("savePosX", this.transform.position.x);
        SaveFloat("savePosY", this.transform.position.y);
        SaveFloat("savePosZ", this.transform.position.z);
        SaveFloat("saveRotX", player.transform.rotation.x);
        SaveFloat("saveRotY", player.transform.rotation.y);
        SaveFloat("saveRotZ", player.transform.rotation.z);
        SaveFloat("saveRotW", player.transform.rotation.w);
        SaveString("saveSavePoint", myName);

        // マジカメなどの情報もOK
    }
}