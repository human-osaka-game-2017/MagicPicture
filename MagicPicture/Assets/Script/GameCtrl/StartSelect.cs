using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSelect : MonoBehaviour {

    private Vector3 pos;

    // Use this for initialization
    void Start () {
        pos.x = -1.10f;              //-8.0
	}
	
	// Update is called once per frame
	void Update () {

        float vertaxis = Input.GetAxis("VerticalForMove");
        
        if (vertaxis < 0) {
            GameState.SetGameState((int)state.load);

            pos.y = -2.6f;          //1.25

            transform.position = pos;
        }
        if (vertaxis > 0) {
            GameState.SetGameState((int)state.beginning);

            pos.y = -1.35f;          //3.15

            transform.position = pos;
        }


        //丸ボタンを押したら
        if (Input.GetButtonDown("ForSilhouetteMode"))
        {
            SoundManager.GetInstance().Play("SE_Click", SoundManager.PLAYER_TYPE.NONLOOP, true);
            SceneManager.LoadScene("MstageRetake");
        }
    }
}