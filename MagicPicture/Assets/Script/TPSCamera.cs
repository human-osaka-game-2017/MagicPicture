using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour {

    private Vector3 offset = Vector3.zero;
    private GameObject player = null;

    // Use this for initialization
    void Awake () {
        this.player = GameObject.Find("Player");
        offset = this.transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position + offset;
    }
}