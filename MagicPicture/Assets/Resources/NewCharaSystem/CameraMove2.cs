using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2 : MonoBehaviour {

    Vector3     pos;
    GameObject  player;    

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update () {
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y + 3;
        pos.z = player.transform.position.z - 4;

        transform.position = pos;
    }
}
