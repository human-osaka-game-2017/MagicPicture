using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    Vector3             rotation;
    Rigidbody           RigidBody;
    int                 canRotationCount;
    public static bool  rotationFlag;
    
    // Use this for initialization
    void Start () {
        RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        // 落下中は操作不可
        if (RigidBody.useGravity) {
            canRotationCount = 0;
        }
        if (RigidBody.useGravity == false) {
            canRotationCount++;
        }
        
        // 地面に着地している確認が取れたら回転(誤差分)
        if (canRotationCount == 10) {
            Rotation();
            canRotationCount = 9;
        }
    }


    void Rotation()
    {
        if (Input.GetKey("left")) {
            rotation.z = -1.5f;
        }
        if (Input.GetKey("right")) {
            rotation.z = 1.5f;
        }
        
        if (rotation.z != 0) {
            rotationFlag = true;
        }
        
        if (!Input.GetKey("left") && !Input.GetKey("right")) {
            rotation.z = 0;
            rotationFlag = false;
        }
        
        transform.Rotate(rotation);
    }
}