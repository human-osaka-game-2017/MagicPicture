using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public enum STATE
    {
        TITLE,
        LOAD,
        PLAY,
        DEATH,
        CLEAR
    }

    public bool       debugState;
    public static int state;

	// Use this for initialization
	void Start () {
        // 自分のシーンでplayから始めるための処理
        // タイトルシーンから始める場合コメントアウト化
        if (debugState) state = 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public static int GetState()
    {
        return state;
    }

    public static void SetState(int _set)
    {
        state = _set;
    }
}