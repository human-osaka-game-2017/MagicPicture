using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_BoardCtrl : MonoBehaviour {

    private Rigidbody rigidbodyComponent;
    private float     resetTimer;
    private bool      exitFlag;

    // Use this for initialization
    void Start () {
        rigidbodyComponent = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        // 落ちていったらDestroy
        if (transform.position.y < -20) {
            Destroy(gameObject);
        }

        // freezeされたらfreezeを無効化へ
        if (resetTimer > 0) {
            resetTimer += Time.deltaTime;
            if (exitFlag && resetTimer > 1) {
                exitFlag = false;
                resetTimer = 0;

                // Rigitbodyのfreezeを無効化する
                rigidbodyComponent.constraints = RigidbodyConstraints.None;
            }
        }
	}


    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player") {
            rigidbodyComponent.constraints = RigidbodyConstraints.FreezeAll;

            resetTimer = 0.1f;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player") {
            exitFlag = true;
        }
    }
}