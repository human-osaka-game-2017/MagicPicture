using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainButtonTrigger : MonoBehaviour {

    [SerializeField] ActionCtrl  actionCtrl;
    [SerializeField] private int buttonNum;

    private int  state = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if ((buttonNum + 1) == state) {
            actionCtrl.Action();
        }
        else {
            actionCtrl.Reset();
        }
	}
    
    public int GetState()
    {
        return state;
    }

    public void SetState(int _state)
    {
        state = _state;
    }
}