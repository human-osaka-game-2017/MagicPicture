using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGimmickSpear : MonoBehaviour {
    
    public  bool onFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Colliderを非アクティブにして矢の発射を止める
        if (HitCtrl.gameOverState == (int)gameState.e_HitSpear) {
            onFlag = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }


    //===========================
    // Playerが感圧板に触れたら
    //===========================
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "PlayerCol") {
            onFlag = true;
        }
    }
    

    //=============================
    // Playerが感圧板から離れたら
    //=============================
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "PlayerCol") {
            onFlag = false;
        }
    }


    public bool GetOnFlag()
    {
        return onFlag;
    }
}