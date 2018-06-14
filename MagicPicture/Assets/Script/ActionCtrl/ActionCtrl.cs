using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCtrl : MonoBehaviour {

    [SerializeField] bool                 door;
    [SerializeField] private DoorAction   doorAction;

    [SerializeField] bool                 effect;
    [SerializeField] private EffectAction effectAction;

    [SerializeField] bool                 spear;
    [SerializeField] private SpearAction  spearAction;

    [SerializeField] bool                 wall;
    [SerializeField] private WallAction   wallAction;

    [SerializeField] bool                 notActive;
    [SerializeField] private ActiveAction actionActive;


    private bool playOnceFlag;
    
    // 非アクティブ
    // 道わけ

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public void Action()
    {
        if (door) {
            doorAction.Open();
        }
        if (effect && !playOnceFlag) {
            playOnceFlag = true;
            effectAction.EffectPlay();
        }
        if (spear) {
            spearAction.Shot();
        }
        if (wall) {
            wallAction.RotWall();
        }
        if (notActive) {
            actionActive.NotActive();
        }
    }

    virtual public void Reset()
    {
        if (door) {
            doorAction.Close();
        }
        if (effect) {
            playOnceFlag = false;
            effectAction.EffectStop();
        }
        if (wall) {
            wallAction.RotReset();
        }
        if (spear) {
            spearAction.Stop();
        }
    }
}