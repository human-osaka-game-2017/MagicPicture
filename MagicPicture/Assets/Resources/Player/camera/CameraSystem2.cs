using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem2 : MonoBehaviour {

    Vector3             cameraPos;
    Vector3             rotation;
    GameObject          player;
    public static bool  changeMode;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r")) {

            if (!changeMode) {
                changeMode = true;
            }
            else if (changeMode) {
                changeMode = false;
            }
        }

        
        if (changeMode) GiveFPSMode();
        if (!changeMode) GiveTPSMode();


        Rotation();
        transform.position = cameraPos;        
    }


    //===================
    // Playerとそろえる
    //===================
    void Rotation()
    {
        if (Input.GetKey("left")) {
            rotation.y -= 1.5f;
        }
        if (Input.GetKey("right")) {
            rotation.y += 1.5f;
        }
    }


    //============
    // TPSモード
    //============
    void GiveTPSMode()
    {
        cameraPos.x = player.transform.position.x;
        cameraPos.y = player.transform.position.y + 3;
        cameraPos.z = player.transform.position.z - 4;

        transform.rotation = Quaternion.Euler(20, 0, 0);

        rotation.x = 0;
    }


    //============
    // FPSモード
    //============
    void GiveFPSMode()
    {
        cameraPos.x = player.transform.position.x;
        cameraPos.y = player.transform.position.y + 1;
        cameraPos.z = player.transform.position.z;

        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);


        if (Input.GetKey("up")) {
            rotation.x -= 1.5f;
        }
        if (Input.GetKey("down")) {
            rotation.x += 1.5f;
        }
        if (Input.GetKey("q")) {
            rotation.x = 0;     // カメラ上下回転リセット
        }
    }
}