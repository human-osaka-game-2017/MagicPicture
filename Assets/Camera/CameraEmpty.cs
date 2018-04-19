using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmpty : MonoBehaviour {

    float Rot_Y = 0;
    int count = 0;
    int maxCount = 18;
    int KeyDownNum = 0;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        RotationLittle();
        transform.Rotate(new Vector3(0, Rot_Y, 0));
    }

    void PursuitMove()
    {
        if (Input.GetKeyDown("right")) {
            KeyDownNum = 1;
        }
        if (Input.GetKeyDown("left")) {
            KeyDownNum = 2;
        }
    }

    //-------------
    // カメラ親回転
    void Rotation()
    {
        switch (KeyDownNum)
        {
            case 1:
                transform.Rotate(new Vector3(0, 90 / 18, 0));
                count++;
                break;
            case 2:
                transform.Rotate(new Vector3(0, -90 / 18, 0));
                count++;
                break;
            default:
                break;
        }
    }

    void RotationLittle()
    {
        if (Input.GetKey("a")) {
            Rot_Y = -3;
        }
        if (Input.GetKey("d")) {
            Rot_Y = 3;
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            Rot_Y = 0;
        }
        if (Input.GetKeyDown("s")) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void ReMove()
    {
        if (count == 18) {
            count = 0;
            KeyDownNum = 0;
        }
    }
}