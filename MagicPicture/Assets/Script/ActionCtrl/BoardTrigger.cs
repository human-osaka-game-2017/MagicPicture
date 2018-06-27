using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTrigger : MonoBehaviour {

    [SerializeField] ActionCtrl actionCtrl;

    private List<GameObject> onRidingObj = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        List<GameObject> tmp = new List<GameObject>();
        foreach(var obj in onRidingObj)
        {
            if (obj == null)
            {
                tmp.Add(obj);
            }
        }
        foreach(var obj in tmp)
        {
            onRidingObj.Remove(obj);
            actionCtrl.Reset();
        }
    }

    //-------------------
    // 感圧板を踏んだら
    void OnTriggerStay(Collider col)
    {
        // 現像前のobjectに当たってもスルー(layerの2)
        if (col.gameObject.layer != 2) {
            if (this.onRidingObj.Find(obj => obj.gameObject == col.gameObject) == null)
            {
                this.onRidingObj.Add(col.gameObject);
            }
            actionCtrl.Action();
        }
    }
    
    //---------------------
    // 感圧板から離れたら
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer != 2) {
            if (this.onRidingObj.Find(obj => obj.gameObject == col.gameObject) != null)
            {
                this.onRidingObj.Remove(col.gameObject);
                actionCtrl.Reset();
            }
        }
    }
}