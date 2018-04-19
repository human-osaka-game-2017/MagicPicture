using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    bool Count = false;
    int KeyDownNum = 0;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject Chara = GameObject.Find("Chara");

        float x = Chara.transform.position.x;
        float y = Chara.transform.position.y;
        float z = Chara.transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}
