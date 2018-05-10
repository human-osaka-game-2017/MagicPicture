using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewGame : MonoBehaviour {

    private Button newButton;

    // Use this for initialization
    void Start () {
        newButton = GetComponent<Button>();
        newButton.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {

    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
       // PlayerMove.sceneDivergence = (int)Scene.e_Gameing;
    }
}