using UnityEngine;

public class Suicide : MonoBehaviour {

    [SerializeField]private float time;

	void Start () {
        Destroy(this.gameObject, time);
	}
	
	void Update () {
		
	}
}
