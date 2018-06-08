using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBDCtrl : MonoBehaviour {

    [SerializeField] GameObject    doorR;
    [SerializeField] GameObject    doorL;
    [SerializeField] GameObject    cantBack;
    [SerializeField] ChainButton_1 chainButton_1;
    [SerializeField] ChainButton_2 chainButton_2;
    [SerializeField] ChainButton_3 chainButton_3;

    public float speed;
    public float openRange;
    public float closeRange;
    public  bool closeFlag;    
    private int  openState;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        if (chainButton_3.onButton) {
            if (openState == 2) openState = 3;
        }
        else if (chainButton_2.onButton) {
            if (openState == 1) openState = 2;
        }
        else if (chainButton_1.onButton) {
            openState = 1;
        }
    }


    void FixedUpdate()
    {
        // いろいろ含めた速度
        float mixSpeed = Time.deltaTime * speed;

        // ボタンが順番に押されたらオープン
        if (openState == 3 && !closeFlag) {

            // cantBackを無効化
            cantBack.GetComponent<Collider>().isTrigger = true;
            
            if (!chainButton_1.onButton) openState = 0;
            if (!chainButton_2.onButton) openState = 1;
            if (!chainButton_3.onButton) openState = 2;

            if (doorR.transform.localPosition.x < openRange) {
                doorR.transform.Translate(Vector3.right * mixSpeed);
                doorL.transform.Translate(Vector3.left  * mixSpeed);
            }
        }
        else if (openState != 3 || closeFlag) {
            if (doorR.transform.localPosition.x > closeRange) {
                doorR.transform.Translate(Vector3.left  * mixSpeed);
                doorL.transform.Translate(Vector3.right * mixSpeed);
            }
        }
    }
}