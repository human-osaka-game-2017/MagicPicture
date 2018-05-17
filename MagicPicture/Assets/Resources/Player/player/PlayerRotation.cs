using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private Vector3 rotation;
    public float    rotarySpeed = 1.5f;
    
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update() {

        if (!PlayerMove.GetStopperFlag())

        Rotation();
    }

    void FixedUpdate()
    {
        transform.Rotate(rotation);

        rotation.y = 0;
    }


    //=================
    // プレイヤー回転
    //=================
    void Rotation()
    {
        if (Input.GetKey("left")) {
            rotation.y = -rotarySpeed;
        }
        if (Input.GetKey("right")) {
            rotation.y = rotarySpeed;
        }
    }
}