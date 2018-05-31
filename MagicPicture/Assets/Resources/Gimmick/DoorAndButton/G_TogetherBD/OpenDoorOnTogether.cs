using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnTogether : MonoBehaviour {

    [SerializeField] GameObject     doorR;
    [SerializeField] GameObject     doorL;
    [SerializeField] GameObject     cantBack;
    [SerializeField] OnTogetherCtrl ctrl;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        // Dummyはスルー

        if (ctrl.closeFlag) {
            ctrl.onButtonCount = 0;
        }
        
        // いろいろ含めた速度
        float mixSpeed = Time.deltaTime * ctrl.speed;

        // 正解ボタンが全て押されたら
        if (ctrl.onButtonCount >= ctrl.correctButtonNum) {

            // ドアを開く
            if (doorR.transform.localPosition.x < ctrl.openRange) {
                doorR.transform.Translate(Vector3.right * mixSpeed);
                doorL.transform.Translate(Vector3.left  * mixSpeed);
            }
        }

        // 正解ボタンが全て押されてなかったら、または解除
        if (ctrl.onButtonCount != ctrl.correctButtonNum) {

            ctrl.closeFlag = false;

            // ドアを閉じる
            if (doorR.transform.localPosition.x > ctrl.closeRange) {
                
                doorR.transform.Translate(Vector3.left  * mixSpeed);
                doorL.transform.Translate(Vector3.right * mixSpeed);
            }
        }
    }


    //=============================================
    // 正解ボタンが全て押されたら完全にtrueになる
    //=============================================
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Player") {
            ctrl.onButtonCount++;

            cantBack.GetComponent<Collider>().isTrigger = true;
        }
    }


    //======================================
    // 全て押されてなかったらfalseが混ざる
    //======================================
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name != "Player") {
            if (ctrl.onButtonCount > 0) {
                ctrl.onButtonCount--;
            }
        }
    }
}