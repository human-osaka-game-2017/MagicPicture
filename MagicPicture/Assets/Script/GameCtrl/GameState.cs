using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum state
{
    title,
    beginning,
    load
}

public class GameState : MonoBehaviour {
    
    public static int gameState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static int GetGameState()
    {
        return gameState;
    }

    public static void SetGameState(int _set)
    {
        gameState = _set;
    }
}