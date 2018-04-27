using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrigger : MonoBehaviour {

    public bool SpearOn;

	// Use this for initialization
	void Start () {
        SpearOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "a")  //タグをプレイヤーに置き換える
            SpearOn = true;
       // Debug.Log("スピアートリガー");
    }
}
