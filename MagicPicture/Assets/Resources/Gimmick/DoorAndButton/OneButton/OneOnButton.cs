using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOnButton : MonoBehaviour {

    private static int num = Num.DBnum;

    public static bool[]    OpenInform = new bool[num];

    private string[]        buttonName = new string[num];
    private GameObject[]    button = new GameObject[num];

    // Use this for initialization
    void Start () {

        for (int i = 0; i < num; i++) {
            buttonName[i] = "Button" + i;
            button[i] = GameObject.Find(buttonName[i]);
        }
    }

    // Update is called once per frame
    void Update() {

        float speed = Time.deltaTime / 15;

        for (int i = 0; i < num; i++) {
            if (OpenInform[i]) {    // ドアを開く
                if (button[i].transform.localPosition.y > 0.25f) {
                    button[i].transform.Translate(Vector3.down * speed);
                }
            }
            if (!OpenInform[i]) {   // ドアを閉める
                if (button[i].transform.localPosition.y < 0.75f) {
                    button[i].transform.Translate(Vector3.up * speed);
                }
            }
        }
    }

    //=========================
    // もしボタンが押されたら
    //=========================
    void OnCollisionEnter(Collision col)
    {
        for (int i = 0; i < num; i++) {
            if (col.gameObject.name == buttonName[i]) {
                OpenInform[i] = true;
            }
        }
    }


    //=========================
    // もしボタンが離されたら
    //=========================
    void OnCollisionExit(Collision col)
    {
        for (int i = 0; i < num; i++) {
            if (col.gameObject.name == buttonName[i]) {
                OpenInform[i] = false;
            }
        }
    }


    public static bool GetOpenInform(int _i)
    {
        return OpenInform[_i];
    }
    
    public static void SetOpenInform(int _i, bool _set)
    {
        OpenInform[_i] = _set;
    }
}