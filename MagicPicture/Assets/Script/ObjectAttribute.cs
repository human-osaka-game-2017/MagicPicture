using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttribute : MonoBehaviour {

    [SerializeField] private bool canTake;
    public bool CanTake
    {
        get { return this.canTake; }
    }

    private bool isTakenObj;
    public bool IsTakenObj
    {
        get { return this.IsTakenObj; }
        //set
    }
    public void Taken()
    {
        this.isTakenObj = true;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
