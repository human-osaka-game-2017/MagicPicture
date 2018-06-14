using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSelector : MonoBehaviour {

    private int     select;
    private Vector3 pos;

    // Use this for initialization
    void Start () {
        pos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

        float verticality = Input.GetAxis("VerticalForMove");

        if (verticality > 0) {
            select = 0;
        }
        if (verticality < 0) {
            select = 1;
        }

        if (select == 0) {
            pos.y = -90;
            transform.localPosition = pos;
        }
        if (select == 1) {
            pos.y = -145;
            transform.localPosition = pos;
        }

        if (Input.GetButtonDown("ForSilhouetteMode")) {

            SoundManager.GetInstance().Play("SE_Click", SoundManager.PLAYER_TYPE.NONLOOP, true);

            if (select == 0) GoPlay();
            if (select == 1) GoLoading();
        }
    }


    void GoLoading()
    {
        // GameStateをロードに
        GameState.SetState((int)state.load);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("Funaoka");
    }

    void GoPlay()
    {
        // GameStateをプレイに
        GameState.SetState((int)state.play);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("Funaoka");
    }
}