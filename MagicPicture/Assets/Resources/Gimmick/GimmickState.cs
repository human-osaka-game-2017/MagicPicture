using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickState : MonoBehaviour {

    public bool open;
    public bool disappear;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }


    virtual public void Action()
    {
        if (open) {

        }
        if (disappear) {
            gameObject.SetActive(false);
        }
    }
}