using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSelector : MonoBehaviour {

    [SerializeField] private float[] elemHeight;

    private int     select;
    private bool    actionFlag;
    private Vector3 pos;
    

    // Use this for initialization
    void Start () {
        pos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        
        float verticality = Input.GetAxis("VerticalForMove");
        
        if (!actionFlag) {
            if (verticality < 0) {
                if (select < 2) select++;
            }
            if (verticality > 0) {
                if (select > 0) select--;
            }
        }

        // Selectorが動きすぎないように制御
        if (verticality != 0) actionFlag = true;
        if (verticality == 0) actionFlag = false;
        

        if (select == 0) pos.y = elemHeight[0]; // -90f
        if (select == 1) pos.y = elemHeight[1]; // -142f
        if (select == 2) pos.y = elemHeight[2]; // -180f

        transform.localPosition = pos;
        
        if (Input.GetButtonDown("ForSilhouetteMode")) {

            SoundManager.GetInstance().Play("SE_Click", SoundManager.PLAYER_TYPE.NONLOOP, true);

            if (select == 0) GoPlay();
            if (select == 1) GoLoading();
            if (select == 2) GoQuit();
        }
    }


    void GoLoading()
    {
        // GameStateをロードに
        GameState.SetState((int)state.load);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("MasterMain");
    }

    void GoPlay()
    {
        // GameStateをプレイに
        GameState.SetState((int)state.play);

        // 再読み込みでsceneリセットかつロード
        SceneManager.LoadScene("MasterMain");
    }

    void GoQuit()
    {
        // アプリケーション終了
        Application.Quit();
    }
}