using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStepBottum : MonoBehaviour {

    [SerializeField] private ExternalFactor player;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != player.name) {
            player.hitStepFlag = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name != player.name) {
            player.hitStepFlag = false;
        }
    }
}