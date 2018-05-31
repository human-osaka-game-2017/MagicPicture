using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpProcessing : MonoBehaviour {

    [SerializeField] GameObject tmpBox;

    public bool okFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (okFlag) {
            tmpBox.transform.Rotate(0, 5, 0);

            okFlag = false;
        }
    }
}