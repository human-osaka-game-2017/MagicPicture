using UnityEngine;

public class AxisUpdater : MonoBehaviour {

    AxisStateManager manager;

	// Use this for initialization
	void Start () {
        manager = AxisStateManager.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
        manager.Update();
    }
}
