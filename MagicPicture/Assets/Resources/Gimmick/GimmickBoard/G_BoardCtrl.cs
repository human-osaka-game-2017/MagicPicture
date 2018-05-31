using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_BoardCtrl : MonoBehaviour {
    
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        // 落ちていったらDestroy
        if (transform.position.y < -20) {
            Destroy(gameObject);
        }
	}
}