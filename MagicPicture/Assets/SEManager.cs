using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {

    private bool playOnce;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public void Play(string _SEName)
    {
        if (!playOnce) {
            playOnce = true;
            SoundManager.GetInstance().Play(_SEName, SoundManager.PLAYER_TYPE.NONLOOP, true);
        }
    }

    virtual public void Reuse()
    {
        playOnce = false;
    }
}