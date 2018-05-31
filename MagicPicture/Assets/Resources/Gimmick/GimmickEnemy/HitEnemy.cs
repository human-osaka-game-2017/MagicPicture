using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitEnemy : MonoBehaviour {

    [SerializeField] GameOverScene gameOverUI;

    public float damageMotionTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (HitCtrl.gameOverState == (int)gameState.e_HitEnemy) {

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
        
        gameOverUI.gameObject.SetActive(true);
    }
}