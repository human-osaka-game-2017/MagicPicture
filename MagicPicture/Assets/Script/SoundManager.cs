using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SoundManager
{
    public enum PLAYER_TYPE
    {
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
        this.soundPlayers = new AudioSource[(int)PLAYER_TYPE.COUNT];
    }

    public void RegisterSoundPlayer(int playerIndex, AudioSource audioSource)
    {
        this.soundPlayers[playerIndex] = audioSource;
    }

    public void SetSoundSourceInPlayer(string fileName, int playerIndex)
    {
        this.soundPlayers[playerIndex].clip = this.soundTable[fileName];
    }

    public void Load(string fileName)
    {
        string filePath = "Sounds/" + fileName;
        this.soundTable[fileName] = Resources.Load(filePath) as AudioClip;
    }

    public void Play(string fileName, int playerIndex, bool soundOverlap)
    {
        if (!this.soundTable.ContainsKey(fileName))
        {
            Load(fileName);
        }

        if (soundOverlap) this.soundPlayers[playerIndex].PlayOneShot(this.soundTable[fileName]);
        else
        {
            if(this.soundPlayers[playerIndex].clip != this.soundTable[fileName])
                SetSoundSourceInPlayer(fileName, playerIndex);
            
            this.soundPlayers[playerIndex].Play();
        }
    }

    public void Stop(int playerIndex)
    {
        this.soundPlayers[playerIndex].Stop();
    }

    public void Pause(int playerIndex)
    {
        this.soundPlayers[playerIndex].Pause();
    }

    public void Resume(int playerIndex)
    {
        this.soundPlayers[playerIndex].UnPause();
    }
}