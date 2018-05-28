using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitEnemy : MonoBehaviour {

    [SerializeField] GameOverScene gameOverScene;

    public float damageMotionTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //=================================
    // 敵キャラとプレイヤーの衝突判定
    //=================================
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player") {
            PlayerCtrl.SetStopperFlag(true);

            StartCoroutine("WaitGoGameOver");
        }
    }


    //===================
    // ゲームオーバーへ
    //===================
    IEnumerator WaitGoGameOver()
    {
        // モーション分待ってゲームオーバーへ
        yield return new WaitForSeconds(damageMotionTime);

        gameOverScene.gameObject.SetActive(true);
    }
}