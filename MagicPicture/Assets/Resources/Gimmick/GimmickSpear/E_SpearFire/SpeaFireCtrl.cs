using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeaFireCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public void Action()
    {
        GetComponent<ParticleSystem>().Play();
    }
}