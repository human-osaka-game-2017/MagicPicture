using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoClearScene : MonoBehaviour {

    [SerializeField] GameClearUI gameClearUI;

    public  float   joyMotionTime;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (HitCtrl.gameClearFlag) {

            // 丸ボタンを押したら演出スキップ
            //if (Input.GetButtonDown("Fire3")) {
            //    SceneManager.LoadScene("ClearScene");
            //}

            StartCoroutine("WaitGoClearScene");
        }
    }


    //===================
    // ゲームオーバーへ
    //===================
    IEnumerator WaitGoClearScene()
    {
        SoundManager.GetInstance().Play("SE_GameOver", SoundManager.PLAYER_TYPE.NONLOOP, true);

        // GameClearUIをアクティブ化
        gameClearUI.gameObject.SetActive(true);
        
        // モーション分待ってゲームオーバーへ
        yield return new WaitForSeconds(joyMotionTime);

        SceneManager.LoadScene("ClearSceneM");
    }
}