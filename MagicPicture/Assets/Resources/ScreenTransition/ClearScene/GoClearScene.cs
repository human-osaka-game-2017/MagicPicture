using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoClearScene : MonoBehaviour {

    [SerializeField]
    GameClearUI     gameClearUI;

    public float    joyMotionTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }


    //=================================
    // ClearObjとプレイヤーの衝突判定
    //=================================
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            PlayerMove.SetStopperFlag(true);
            
            StartCoroutine("WaitGoClearScene");
        }
    }


    //===================
    // ゲームオーバーへ
    //===================
    IEnumerator WaitGoClearScene()
    {
        Debug.Log("喜びモーション中(仮)");

        // GameClearUIをアクティブ化
        gameClearUI.gameObject.SetActive(true);

        // モーション分待ってゲームオーバーへ
        yield return new WaitForSeconds(joyMotionTime);

        SceneManager.LoadScene("ClearScene");
    }
}