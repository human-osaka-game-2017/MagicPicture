using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverSelect : MonoBehaviour {

    private RectTransform selectPoint;
    private Vector3 pos;
    private int     select = 2;

    // Use this for initialization
    void Start () {
        selectPoint = GameObject.Find("SelectPoint").GetComponent<RectTransform>();

        pos = selectPoint.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

        float horzaxis = 0;

        if (PlayerCtrl.GetStopperFlag()) {
            horzaxis = Input.GetAxis("Horizontal");
        }

        // スティックを左に倒したら
        if (horzaxis < 0) {
            select = 1;
            pos.x = -330;

            selectPoint.localPosition = pos;
        }
        // スティックを右に倒したら
        if (horzaxis > 0) {
            select = 2;
            pos.x = 20;

            selectPoint.localPosition = pos;
        }


        // 丸ボタンを押したら
        if (Input.GetButtonDown("Fire3")) {
            if (select == 1) GoQuit();
            if (select == 2) GoLoading();
        }
    }


    private void GoLoading()
    {
        // GameStateをロードに
        GameState.SetGameState((int)state.load);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("Funaoka");
    }

    private void GoQuit()
    {
        // アプリケーション終了
        Application.Quit();
    }
}