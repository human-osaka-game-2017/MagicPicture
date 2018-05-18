using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombOnButton3 : MonoBehaviour {

    public static bool  onButton;

    private bool        downButtonFlag;
    private int         downButtonCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (onButton) {
            if (!downButtonFlag) {
                downButtonCount++;

                if (downButtonCount <= 200) {
                    transform.localPosition -= new Vector3(0, 0.0051f, 0);
                }
                else {
                    downButtonFlag = true;
                }
            }
        }
        else {
            if (downButtonCount > 0) {
                downButtonCount--;
                downButtonFlag = false;

                transform.localPosition += new Vector3(0, 0.0051f, 0);
            }
        }
    }


    //=========================
    // もしボタンが押されたら
    //=========================
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "InorganicMatterTag") {
            onButton = true;
        }
    }


    //=========================
    // もしボタンが離されたら
    //=========================
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "InorganicMatterTag") {
            onButton = false;
        }
    }


    public static bool GetOnNum()
    {
        return onButton;
    }

    public static void SetOnFlag(bool _set)
    {
        onButton = _set;
    }
}