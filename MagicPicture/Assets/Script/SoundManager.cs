using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SoundManager
{
    public enum PLAYER_TYPE
    {
        LOOP,
        NONLOOP,
        COUNT
    }

    static SoundManager instance = null;

    public static SoundManager GetInstance()
    {
        return instance ?? (instance = new SoundManager());
    }

    // サウンド再生のためのゲームオブジェクト
    GameObject obj = null;

    // 再生プレイヤー
    AudioSource[] soundPlayers;

    // Soundにアクセスするためのテーブル 
    Dictionary<string, AudioClip> soundTable = new Dictionary<string, AudioClip>();

    private SoundManager()
    {
        if (this.obj == null)
        {
            this.obj = new GameObject("SoundManager");
            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(this.obj);
        }

        this.soundPlayers = new AudioSource[(int)PLAYER_TYPE.COUNT];
        for(int i=0; i< this.soundPlayers.Length; ++i)
        {
            this.soundPlayers[i] = this.obj.AddComponent<AudioSource>();
        }
        this.soundPlayers[(int)PLAYER_TYPE.LOOP].loop = true;
        this.soundPlayers[(int)PLAYER_TYPE.NONLOOP].loop = false;
    }

    public void RegisterSoundPlayer(PLAYER_TYPE playerType, AudioSource audioSource)
    {
        this.soundPlayers[(int)playerType] = audioSource;
    }

    public void SetSoundSourceInPlayer(string fileName, PLAYER_TYPE playerType)
    {
        this.soundPlayers[(int)playerType].clip = this.soundTable[fileName];
    }

    public void Load(string fileName)
    {
        string filePath = "Sounds/" + fileName;
        this.soundTable[fileName] = Resources.Load(filePath) as AudioClip;
    }

    public void Play(string fileName, PLAYER_TYPE playerType, bool soundOverlap)
    {
        if (!this.soundTable.ContainsKey(fileName))
        {
            Load(fileName);
        }

        if (soundOverlap) this.soundPlayers[(int)playerType].PlayOneShot(this.soundTable[fileName]);
        else
        {
            if(this.soundPlayers[(int)playerType].clip != this.soundTable[fileName])
                SetSoundSourceInPlayer(fileName, playerType);
            
            this.soundPlayers[(int)playerType].Play();
        }
    }

    public void Stop(PLAYER_TYPE playerType)
    {
        this.soundPlayers[(int)playerType].Stop();
    }

    public void Pause(PLAYER_TYPE playerType)
    {
        this.soundPlayers[(int)playerType].Pause();
    }

    public void Resume(PLAYER_TYPE playerType)
    {
        this.soundPlayers[(int)playerType].UnPause();
    }
}