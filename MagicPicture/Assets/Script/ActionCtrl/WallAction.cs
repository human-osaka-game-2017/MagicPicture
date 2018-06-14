using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAction : MonoBehaviour {

    [SerializeField] private float rangeY;
    [SerializeField] private float speed;

    private Vector3 EulerStartRot;

	// Use this for initialization
	void Start () {
        EulerStartRot = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    virtual public void RotWall()
    {
        Vector3 EulerRot = transform.eulerAngles;

        if (EulerRot.y < EulerStartRot.y + rangeY) {
            transform.Rotate(0, speed, 0);
        }
    }


    virtual public void RotReset()
    {
        Vector3 EulerRot = transform.eulerAngles;

        if (EulerRot.y > EulerStartRot.y) {
            transform.Rotate(0, -speed, 0);
        }
    }
}