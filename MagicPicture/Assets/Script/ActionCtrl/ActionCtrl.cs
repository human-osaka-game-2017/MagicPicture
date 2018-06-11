using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCtrl : MonoBehaviour {

    [SerializeField] bool                door;
    [SerializeField] public DoorAction   doorAction;

    [SerializeField] bool                effect;
    [SerializeField] public EffectAction effectAction;

    [SerializeField] bool                spear;
    [SerializeField] public SpearAction  spearAction;

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
        if (spear) {
            spearAction.Stop();
        }
    }
}