using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombOpenDoorL : MonoBehaviour {

    private bool    maxOpenDoorFlag;
    private int     openDoorCount;
    private int     Button;
    
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update ()
    {
        //---------------
        // 全て揃ったら
        if (CombOpenDoorR.openDoorFlag) {
            
            if (!maxOpenDoorFlag) {
                openDoorCount++;

                if (openDoorCount <= 50) {
                    transform.Translate(Vector3.left / 50);
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

                transform.Translate(Vector3.right / 50);
            }
        }
    }
} 