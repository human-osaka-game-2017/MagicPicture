using UnityEngine;

public class AxisUpdater : MonoBehaviour {

    AxisStateManager manager;

	void Start () {
        manager = AxisStateManager.GetInstance();
    }
	
	void Update () {
        manager.Update();
    }
}
