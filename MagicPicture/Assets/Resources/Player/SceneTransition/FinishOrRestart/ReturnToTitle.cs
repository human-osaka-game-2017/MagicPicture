using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReturnToTitle : MonoBehaviour {

    [SerializeField]
    GameOverScene gameOverScene;

    private Button returnButton;

    // Use this for initialization
    void Start () {
        returnButton = GetComponent<Button>();
        returnButton.onClick.AddListener(OnClickButton);
    }
	
	// Update is called once per frame
	void Update () {
        
    }


    //===========================
    // ボタンがクリックされたら
    //===========================
    void OnClickButton()
    {
        gameObject.SetActive(false);
        PlayerMove.sceneDivergence = (int)Scene.e_Start;
    }
}