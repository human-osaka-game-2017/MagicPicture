using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOpenDoor : MonoBehaviour {

    private static int num = Num.DBnum;

    private GameObject[] DoorR = new GameObject[num];
    private GameObject[] DoorL = new GameObject[num];

    // Use this for initialization
    void Start () {

        for (int i = 0; i < num; i++) {
            DoorR[i] = GameObject.Find("DoorR" + i);
            DoorL[i] = GameObject.Find("DoorL" + i);
        }
    }
	
	// Update is called once per frame
	void Update () {

        float speed = Time.deltaTime / 5;

        for (int i = 0; i < num; i++) {
            if (OneOnButton.GetOpenInform(i)) {
                if (DoorR[i].transform.localPosition.x < 2f) {
                    DoorR[i].transform.Translate(Vector3.right * speed);
                    DoorL[i].transform.Translate(Vector3.left * speed);
                }
            }
            if (!OneOnButton.GetOpenInform(i)) {
                if (DoorR[i].transform.localPosition.x > 0.75f) {
                    DoorR[i].transform.Translate(Vector3.left * speed);
                    DoorL[i].transform.Translate(Vector3.right * speed);
                }
            }
        }
	}
}