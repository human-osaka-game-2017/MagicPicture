using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameLoad : MonoBehaviour {
    
    private Button loadButton;

    // Use this for initialization
    void Start () {
        loadButton = GetComponent<Button>();
        loadButton.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {

    }

    
    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        //PlayerMove.sceneDivergence = (int)Scene.e_LoadGame;
    }
}