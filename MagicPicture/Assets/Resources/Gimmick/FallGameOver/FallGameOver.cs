using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGameOver : MonoBehaviour {
    
    [SerializeField]
    GameOverScene   gameOverScene;

    GameObject      Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        FallPlayer();
    }


    //=========================
    // 落下ゲームオーバー判定
    //=========================
    void FallPlayer()
    {
        if (Player.transform.position.y < transform.position.y) {
            PlayerMove.SetStopperFlag(true);

            gameOverScene.gameObject.SetActive(true);
        }
    }
}