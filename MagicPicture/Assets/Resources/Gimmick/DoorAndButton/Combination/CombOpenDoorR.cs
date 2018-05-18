using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombOpenDoorR : MonoBehaviour {

    public static bool  openDoorFlag;
    private bool        maxOpenDoorFlag;
    private int         openDoorCount;
    private int         Button;
    private int[]       enterNum = new int[3];


    public string[] parentName = new string[2];
    private GameObject[,] combDoors = new GameObject[2, 2];


    // Use this for initialization
    void Start () {
        string[] doorNames = new string[2];

        doorNames[0] = "CombDoorR";
        doorNames[1] = "CombDoorL";

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 2; j++) {
                combDoors[i, j] = GameObject.Find(parentName[i] + doorNames[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //---------------------------
        // 押されたボタンを取得する
        /*if (CombOnButton1.GetOnFlag()) {
            Button = 1;
        }
        if (CombOnButton2.GetOnNum()) {
            Button = 2;
        }
        if (CombOnButton3.GetOnNum()) {
            Button = 3;
        }*/

        if (CombOnButton.OnState[0, 0] == 1) {
            Button = 1;
        }
        if (CombOnButton.OnState[0, 1] == 1) {
            Button = 2;
        }
        if (CombOnButton.OnState[0, 2] == 1) {
            Button = 3;
        }


        //---------------------------------
        // 順番で押された時だけ有効にする
        if (enterNum[0] == 0 && enterNum[1] == 0 && enterNum[2] == 0) {
            if (Button == 1) {
                enterNum[0] = 1;
            }
        }
        else if (enterNum[0] == 1 && enterNum[1] == 0 && enterNum[2] == 0) {
            if (Button == 2) {
                enterNum[1] = 2;
            }
        }
        else if (enterNum[0] == 1 && enterNum[1] == 2 && enterNum[2] == 0) {
            if (Button == 3) {
                enterNum[2] = 3;
                openDoorFlag = true;
            }
        }


        //---------------
        // ドアを閉める
        if (CombCloseDoorPanel.closeFlag) {
            openDoorFlag = false;
            CombCloseDoorPanel.closeFlag = false;
            enterNum[0] = enterNum[1] = enterNum[2] = 0;
        }


        //---------------
        // 全て揃ったら
        if (openDoorFlag) {
            
            if (!maxOpenDoorFlag) {
                openDoorCount++;
                
                if (openDoorCount <= 50) {
                    //transform.Translate(Vector3.right / 50);
                    //for (int i = 0; i < 2; i++) {
                        //for (int j = 0; j < 2; j++) {
                            //if (CombOnButton.OnState[0, 0] == 1)
                            /////////combDoors[0, 0]////////////////
                        //}
                    //}
                }
                else {
                    Button = 0;
                    maxOpenDoorFlag = true;
                }
            }
        }
        else {
            if (openDoorCount > 0) {
                openDoorCount--;
                maxOpenDoorFlag = false;

                transform.Translate(Vector3.left / 50);
            }
        }
    }
}