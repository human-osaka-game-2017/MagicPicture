using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorPanel : MonoBehaviour {

    private static int num = Num.DBnum;

    private string[]        closePanelName = new string[num];
    private GameObject[]    closeDoorPanel = new GameObject[num];

    // Use this for initialization
    void Start () {

        for (int i = 0; i < num; i++) {
            closePanelName[i] = "CloseDoorPanel" + i;
            closeDoorPanel[i] = GameObject.Find(closePanelName[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //===================================
    // もしプレイヤーがパネルに触れたら
    //===================================
    void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < num; i++) {
            if (col.gameObject.name == closePanelName[i]) {

                // ドアを閉めてボタンを上げる
                OneOnButton.SetOpenInform(i, false);
            }
        }
    }
}