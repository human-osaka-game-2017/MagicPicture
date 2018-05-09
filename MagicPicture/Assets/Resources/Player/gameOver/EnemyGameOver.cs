using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameOver : MonoBehaviour {

    [SerializeField]
    GameOverScene gameOverScene;

    public Material[] _material;
    int notActiveCount;
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerMove.sceneDivergence == (int)Scene.e_GameOver) {
            if (notActiveCount == 60) {

                GameOverScene meteortmp = Instantiate(gameOverScene);
                meteortmp.gameObject.SetActive(true);
            }
            if (notActiveCount >= 1) {
                notActiveCount++;
            }                   
        }
    }


    //===========================
    // もし敵キャラと衝突したら
    //===========================
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyTag"){
            if (PlayerMove.sceneDivergence != (int)Scene.e_GameOver) {

                // リスタート時にリセットする
                notActiveCount = 1;
                PlayerMove.sceneDivergence = (int)Scene.e_GameOver;
            }
            GetComponent<Renderer>().material = _material[0];
        }
    }
}