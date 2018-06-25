using UnityEngine;

public class ObjectAttribute : MonoBehaviour {

    [SerializeField] private bool canTake;
    public bool CanTake
    {
        get { return (this.canTake && !this.isTakenObj); }
    }

    private bool isTakenObj;
    public void Taken()
    {
        this.isTakenObj = true;
    }

    private bool canPhantom = true;
    public bool CanPhantom
    {
        get { return this.canPhantom; }
    }

    void Start () {
    }
	
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        canPhantom = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canPhantom = true;
    }
}
