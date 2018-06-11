using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttribute : MonoBehaviour {

    [SerializeField] private bool canTake;
    public bool CanTake
    {
        get { return (this.canTake && !this.IsTakenObj); }
    }

    private bool isTakenObj = false;
    public bool IsTakenObj
    {
        get { return this.isTakenObj; }
        //set
    }
    public void Taken()
    {
        this.isTakenObj = true;
    }

    private bool canPhantom = true;
    public bool CanPhantom
    {
        get { return this.canPhantom; }
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        canPhantom = false;
        Debug.Log("enter" + this.gameObject.ToString());
    }

    private void OnTriggerExit(Collider other)
    {
        canPhantom = true;
        Debug.Log("exit" + this.gameObject.ToString());
    }
}
