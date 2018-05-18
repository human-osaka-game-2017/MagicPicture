using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombCloseDoorPanel : MonoBehaviour {
    
    public static bool  closeFlag;

    public string[]         parentName = new string[2];
    private GameObject[]    closePanel = new GameObject[2];

    // Use this for initialization
    void Start () {

        for (int i = 0; i < 2; i++) {
            closePanel[i] = GameObject.Find(parentName[i] + "CloseDoorPanel");
        }
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerStay(Collider col)
    {
        for (int i = 0; i < 2; i++) {
            if (col.gameObject == closePanel[i]) {
                closeFlag = true;
                //Debug.Log("Close");
            }
        }
    }
}