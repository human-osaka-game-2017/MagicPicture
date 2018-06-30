using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BGM : MonoBehaviour, IDisposable {

    //SoundManagerをつかうため音源はResoucesフォルダにあるものでないといけない
    [SerializeField]
    private string audioName;

	void Start ()
    {
        SoundManager.GetInstance().Play(audioName, SoundManager.PLAYER_TYPE.LOOP, false);
	}
	
	void Update ()
    {
		
	}

    public void Dispose()
    {
        SoundManager.GetInstance().Stop(SoundManager.PLAYER_TYPE.LOOP);
    }
}
