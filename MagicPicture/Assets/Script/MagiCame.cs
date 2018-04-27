using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour {

    private GameObject player = null;
    private Ray ray;

    void Start ()
    {
        this.player = GameObject.Find("Player");
        this.ray = new Ray(this.transform.position, this.transform.forward.normalized);

    }

    void Update() {

        this.ray.direction = this.transform.forward.normalized;

        //撮影
        //レイ表示
        Debug.DrawRay(this.ray.origin, this.ray.direction, Color.red);

        if (this.player.GetComponent<CameraSystem>().IsFPSMode)
        {
            // Rayが衝突したコライダーの情報を得る
            RaycastHit collidedObj;

            if (Physics.Raycast(this.ray, out collidedObj))
            {
                //collidedObj.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                //シャッターを切られた
                if (/*!player.m_IsMoving &&*/Input.GetKeyDown(KeyCode.Space))
                {
                    //float distance = collidedObj.distance;

                    this.player.GetComponent<CameraSystem>().SetFilm(Instantiate
                        (collidedObj.collider.gameObject, /*new Vector3()*/transform.position, Quaternion.identity));
                }
            }
        } 
        
    }
}