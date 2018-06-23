using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoTitleButton : MonoBehaviour {
    
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        // Aボタンを押したら演出スキップ
        if (Input.GetButtonDown("ForSilhouetteMode")) {

            // stateをtitleに
            GameState.SetState((int)state.title);

            SceneManager.LoadScene("MasterTitle");
        }
    }
}