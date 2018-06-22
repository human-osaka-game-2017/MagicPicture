using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotActiveAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //-----------------
    // 非アクティブ化
    virtual public void NotActive()
    {
        gameObject.SetActive(false);
    }
}