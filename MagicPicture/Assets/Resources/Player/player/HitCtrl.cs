using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    none,
    hitEnemy,
    hitSpear
}

public class HitCtrl : MonoBehaviour {

    [SerializeField] GameOverScene gameOverUI;

    public  static int  gameState;
    public  static bool gameClearFlag;
    public  float       waitEnemy;
    public  float       waitSpear;
    private float       damageWait;

    // Use this for initialization
    void Start () {
        gameState = (int)State.none;
        gameClearFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (gameState != (int)State.none) {
            StartCoroutine("WaitGoGameOver");
        }
	}
    
    void OnTriggerEnter(Collider col)
    {
        // 画面遷移系

        // Enemyゲームオーバー
        if (col.gameObject.tag == "EnemyTag") {
            PlayerCtrl.SetStopperFlag(true);

            gameState = (int)State.hitEnemy;
            damageWait = waitEnemy;
        }

        // Spearゲームオーバー
        if (col.gameObject.tag == "SpearTag") {

            SoundManager.GetInstance().Play("SE_ArrowHit", SoundManager.PLAYER_TYPE.NONLOOP, true);

            PlayerCtrl.SetStopperFlag(true);
            
            gameState = (int)State.hitSpear;
            GetComponent<Collider>().isTrigger = false;
            damageWait = waitSpear;
        }

        // ゲームクリア
        if (col.gameObject.tag == "GameClearTag") {
            PlayerCtrl.SetStopperFlag(true);

            gameClearFlag = true;
        }
    }


    //-------------------
    // ゲームオーバーへ
    IEnumerator WaitGoGameOver()
    {
        // モーション分待ってゲームオーバーへ
        yield return new WaitForSeconds(damageWait);

        gameOverUI.gameObject.SetActive(true);
    }
}