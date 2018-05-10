using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGameOver : MonoBehaviour {

    [SerializeField]
    GameOverScene   gameOverScene;
    GameObject      m_FallGameOver;

    public Material[] _material;
    int notActiveCount;
    
    // Use this for initialization
    void Start () {
        m_FallGameOver = GameObject.Find("FallGameOver");
    }
	
	// Update is called once per frame
	void Update () {
        //if (PlayerMove.sceneDivergence == (int)Scene.e_GameOver) {
        //    if (notActiveCount == 10) {

        //        GameOverScene meteortmp = Instantiate(gameOverScene);
        //        meteortmp.gameObject.SetActive(true);
        //    }
        //    if (notActiveCount >= 1) {
        //        notActiveCount++;
        //    }
        //}
        GameOversFall();
    }


    //=============================
    // 落下でのゲームオーバー判定
    //=============================
    void GameOversFall()
    {
        float FallHeight = m_FallGameOver.transform.position.y;

        if (transform.position.y < FallHeight) {
            //if (PlayerMove.sceneDivergence != (int)Scene.e_GameOver) {

            //    // リスタート時にリセットする
            //    notActiveCount = 1;
            //    PlayerMove.sceneDivergence = (int)Scene.e_GameOver;
            //}
            GetComponent<Renderer>().material = _material[0];
        }
    }
}