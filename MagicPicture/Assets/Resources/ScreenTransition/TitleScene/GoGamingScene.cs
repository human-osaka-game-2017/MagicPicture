using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoGamingScene : MonoBehaviour {

    private Button button;

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    void Update() {
            
    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        // 静的変数の再設定
        PlayerMove.Reset();

        SceneManager.LoadScene("Bstage");
    }
}