using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerMove.sceneDivergence != (int)Scene.e_Start) {
            Destroy(gameObject);
        }
	}
}