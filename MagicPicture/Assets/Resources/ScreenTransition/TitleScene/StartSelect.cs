using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSelect : MonoBehaviour {

    private Vector3 pos;

    // Use this for initialization
    void Start () {
        pos.x = -7.5f;
	}
	
	// Update is called once per frame
	void Update () {

        float vertaxis = Input.GetAxis("VerticalForMove");
        
        if (vertaxis < 0) {
            GameState.SetGameState((int)state.load);

            pos.y = 1.25f;

            transform.position = pos;
        }
        if (vertaxis > 0) {
            GameState.SetGameState((int)state.beginning);

            pos.y = 2.15f;

            transform.position = pos;
        }


        // 丸ボタンを押したら
        //if (Input.GetButtonDown("Fire3")) {

        //    SceneManager.LoadScene("Funaoka");
        //}
    }
}