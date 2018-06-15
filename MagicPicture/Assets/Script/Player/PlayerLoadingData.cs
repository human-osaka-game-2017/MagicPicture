using System.Text.RegularExpressions;
using UnityEngine;


public class PlayerLoadingData : MonoBehaviour {
    
    private Vector3    loadPos;
    private Quaternion loadRot;
    private string loadSaveName;

    // Use this for initialization
    void Start () {
        
        if (GameState.GetState() == (int)state.load) {
            
            // ロードする要素を呼び出し
            LoadElement();

            // 文字列の数値だけ取得(SavePoint 1, 2, 3...)
            int savePointNum = int.Parse(Regex.Replace(loadSaveName, @"[^0-9]", ""));

            // 最終にhitしたSavePoint含むそれ以下をDestroy
            for (int i = 1; i <= savePointNum; i++) {
                Destroy(GameObject.Find("SavePoint " + i));
            }

            // loadした情報を反映
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
    float LoadFloat(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName, -1);
    }

    string LoadString(string keyName)
    {
        return PlayerPrefs.GetString(keyName, "No Name");
    }
    

    //-------------------
    // ロード要素を列挙
    void LoadElement()
    {
        loadPos.x = LoadFloat("savePosX");
        loadPos.y = LoadFloat("savePosY");
        loadPos.z = LoadFloat("savePosZ");
        loadRot.x = LoadFloat("saveRotX");
        loadRot.y = LoadFloat("saveRotY");
        loadRot.z = LoadFloat("saveRotZ");
        loadRot.w = LoadFloat("saveRotW");
        loadSaveName = LoadString("saveSavePoint");
    }
}