﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //-----------------
    // アクティブ化
    virtual public void Active()
    {
        gameObject.SetActive(true);
    }
}