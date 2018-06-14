using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathSelector : MonoBehaviour {

    private int     select;
    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxis("HorizontalForMove");

        if (horizontal > 0) {
            select = 0;
        }
        if (horizontal < 0) {
            select = 1;
        }

        if (select == 0) {
            pos.x = 20;
            transform.localPosition = pos;
        }
        if (select == 1) {
            pos.x = -330;
            transform.localPosition = pos;
        }

        if (Input.GetButtonDown("ForSilhouetteMode")) {
            if (select == 0) GoLoading();
            if (select == 1) GoQuit();
        }
    }


    void GoLoading()
    {
        // GameStateをロードに
        GameState.SetState((int)state.load);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("Funaoka");
    }

    void GoQuit()
    {
        // アプリケーション終了
        Application.Quit();
    }
}