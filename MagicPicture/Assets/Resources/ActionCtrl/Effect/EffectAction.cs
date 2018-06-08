using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAction : MonoBehaviour {
    
    public bool stopStart;

	// Use this for initialization
	void Start () {
        if (stopStart) {
            GetComponent<ParticleSystem>().Stop();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public void EffectPlay()
    {
        GetComponent<ParticleSystem>().Play();
    }

    virtual public void EffectStop()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}