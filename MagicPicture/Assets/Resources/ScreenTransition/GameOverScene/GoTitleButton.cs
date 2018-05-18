using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoTitleButton : MonoBehaviour {
    
    private Button goTitleButton;

    // Use this for initialization
    void Start () {
        goTitleButton = GetComponent<Button>();
        goTitleButton.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {
        
    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        // TitleSceneをロードする
        SceneManager.LoadScene("TitleScene");
    }
}