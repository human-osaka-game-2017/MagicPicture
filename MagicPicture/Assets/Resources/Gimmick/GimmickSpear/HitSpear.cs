using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpear : MonoBehaviour {

    [SerializeField] GameOverScene  gameOverScene;
    [SerializeField] OnGimmickSpear onGimmickSpear;

    public float damageMotionTime;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //=============================
    // プレイヤーに槍が当たったら
    //=============================
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player") {

            // プレイヤーを動けなくする
            PlayerCtrl.SetStopperFlag(true);

            // 感圧板のisTriggerをfalseにして槍の発射を止める
            onGimmickSpear.GetComponent<Collider>().isTrigger = false;

            // ゲームオーバーUIを表示
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