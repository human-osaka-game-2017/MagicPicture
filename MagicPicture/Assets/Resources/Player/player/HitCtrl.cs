using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum gameState
{
    e_None,
    e_HitEnemy,
    e_HitSpear
}

public class HitCtrl : MonoBehaviour {
    
    public static int  gameOverState;
    public static bool gameClearFlag;

    // Use this for initialization
    void Start () {
        gameOverState = (int)gameState.e_None;
        gameClearFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider col)
    {
        // セーブ、感圧板は別

        // Enemyゲームオーバー
        if (col.gameObject.tag == "EnemyTag") {
            PlayerCtrl.SetStopperFlag(true);

            gameOverState = (int)gameState.e_HitEnemy;
        }

        // Spearゲームオーバー
        if (col.gameObject.tag == "SpearTag") {
            PlayerCtrl.SetStopperFlag(true);
            
            gameOverState = (int)gameState.e_HitSpear;
        }

        // ゲームクリア
        if (col.gameObject.tag == "GameClearTag") {
            PlayerCtrl.SetStopperFlag(true);

            gameClearFlag = true;
        }
    }
}