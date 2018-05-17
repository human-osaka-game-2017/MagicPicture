using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombOnButton : MonoBehaviour {
    
    public string[]         parentName = new string[2];
    public static string[]  tempParentName = new string[2];

    private string[]        buttonName = new string[4];
    public static int[,]    OnState = new int[2, 3];
    private GameObject[,]   combButtons = new GameObject[2, 4];
    private Rigidbody body;

    struct RelatedMove
    {
        public int Counter;
    }

    RelatedMove[,] relatedMove = new RelatedMove[2, 3];

    //GameObject Player;
    //Collider playerCol;

    // Use this for initialization
    void Start () {

        //Player = GameObject.Find("Player");

        //playerCol = Player.GetComponent<Collider>();

        body = GetComponent<Rigidbody>();

        buttonName[0] = "CombButton1";
        buttonName[1] = "CombButton2";
        buttonName[2] = "CombButton3";
        buttonName[3] = "CombCloseDoorPanel"; // この処理は別でやっている

        for (int i = 0; i < 2; i++) {
            tempParentName[i] = parentName[i];

            for (int j = 0; j < 4; j++) {
                combButtons[i, j] = GameObject.Find(parentName[i] + buttonName[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //pub_OnState = OnState;
    }

    float time = 0;
    bool flag;


    // パネルを踏んだら
    /*if (Input.GetKeyDown("c")) {
        OnState[0, 0] = 2;
        body.drag = 5;
        combButtons[0, 0].GetComponent<Collider>().isTrigger = false;
    }*/




    void FixedUpdate()
    {
        if (flag) time += Time.deltaTime;

        if (body.drag > 0) {
            body.drag -= 0.1f;
        }
        else {
            body.drag = 0;
        }

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                if (time > 5) {
                    combButtons[i, j].GetComponent<Collider>().isTrigger = true;

                    if (j == 2) {
                        flag = false;
                        time = 0;
                    }
                }
            }
        }


        for (int i = 0; i < 2; i++) {

            for (int j = 0; j < 3; j++) {
                if (CombCloseDoorPanel.closeFlag) {
                    OnState[i, j] = 2;
                    body.drag = 5;
                    combButtons[i, j].GetComponent<Collider>().isTrigger = false;

                    if (j == 2) {
                        CombCloseDoorPanel.closeFlag = false;
                        flag = true;
                    }
                }
            }

            for (int j = 0; j < 3; j++) {
                
                if (relatedMove[i, j].Counter > 0) {
                    relatedMove[i, j].Counter++;

                    if (relatedMove[i, j].Counter > 30) OnState[i, j] = 2;
                }
                
                if (OnState[i, j] == 1) {
                    if (combButtons[i, j].transform.position.y > 0.25f) {
                        combButtons[i, j].transform.Translate(Vector3.down / 100);
                    }
                }
                else if (OnState[i, j] == 2) {
                    if (combButtons[i, j].transform.position.y < 0.75f) {
                        combButtons[i, j].transform.Translate(Vector3.up / 500);
                    }
                    else {
                        relatedMove[i, j].Counter = 0;
                        OnState[i, j] = 0;
                    }
                }
            }
        }
    }


    //=========================
    // もしボタンが押されたら
    //=========================
    void OnTriggerStay(Collider col)
    {
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                if (col.gameObject == combButtons[i, j]) {
                    OnState[i, j] = 1;
                    relatedMove[i, j].Counter = 0;
                }
            }
        }
    }
    

    //=========================
    // もしボタンが離されたら
    //=========================
    void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                if (col.gameObject == combButtons[i, j]) {
                    OnState[i, j] = 0;
                    relatedMove[i, j].Counter++;
                }
            }
        }
    }

    
    public static string GetParentName(int i)
    {
        return tempParentName[i];
    }
}