using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger_C : MonoBehaviour {

    GameObject doorR;
    GameObject doorL;

    // Use this for initialization
    void Start () {
        doorR = GameObject.Find("doorR");
        doorL = GameObject.Find("doorL");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "a")  //タグをプレイヤーに置き換える
        doorL.transform.Translate(new Vector3(0.5f, 0, 0));
        doorR.transform.Translate(new Vector3(-0.5f, 0, 0));
    }
}
