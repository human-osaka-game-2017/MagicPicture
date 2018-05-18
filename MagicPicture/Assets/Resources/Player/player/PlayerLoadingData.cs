using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingData : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (NewOrLoad.GetLoadFlag()) {
            transform.position = GoLoadGamingScene.GetLoadPos();
            transform.rotation = GoLoadGamingScene.GetLoadRot();

            NewOrLoad.SetLoadFlag(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}