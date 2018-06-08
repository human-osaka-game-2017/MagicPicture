using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpear : MonoBehaviour {

    [SerializeField] GameOverScene  gameOverUI;
    [SerializeField] OnGimmickSpear onGimmickSpear;

    public float damageMotionTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (HitCtrl.gameState == (int)State.hitSpear) {

            // 感圧板のisTriggerをtrueにして槍の発射を止める
            onGimmickSpear.GetComponent<Collider>().isTrigger = true;

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