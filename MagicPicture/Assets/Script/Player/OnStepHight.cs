using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStepHight : MonoBehaviour {

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
            player.cantOnFlag = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name != player.name) {
            player.cantOnFlag = false;
        }
    }
}