using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour
{
    [SerializeField] private float kMinDistance;
    [SerializeField] private float kMaxDistance;

    private GameObject player       = null;
    private FilmManager filmManager = null;
    private PermeationImage img     = null;
    private Vector3 rotSpeed;
    private Ray ray;

    public void Init()
    {
        Quaternion rot = this.transform.rotation;
        rot.x = 0.0f;
        rot.z = 0.0f;
        this.transform.rotation = rot;
    }

    void Start()
    {
        img = GameObject.Find("Shutter").GetComponent<PermeationImage>();
        rotSpeed.x = this.transform.GetComponentInParent<PlayerCtrl>().rotSpeed;
        this.player = GameObject.Find("Player");
        this.filmManager = GameObject.Find("FilmManager").GetComponent<FilmManager>();
        this.ray = new Ray(this.transform.position, this.transform.forward.normalized);
        GameObject.Find("FilmManager").GetComponent<FilmManager>().MaxDistance = kMaxDistance - kMinDistance;
    }

    void Update()
    {
        //更新
        this.ray.direction = this.transform.forward.normalized;
        this.ray.origin = this.transform.position;

        this.transform.Rotate(rotSpeed * Input.GetAxis("VerticalForView") * Time.deltaTime);

        // カメラ上下回転リセット
        //if (Input.GetButtonDown("ForResetCameraView"))
        //{
        //    Init();s
        //}

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
                if (Input.GetButtonDown("ForTakePicture"))
                {
                    img.Init();

                    if (collidedObj.collider.GetComponent<ObjectAttribute>().CanTake)
                    {
                        float distance = collidedObj.distance;
                        if (kMinDistance < distance && distance < kMaxDistance)
                        {
                            filmManager.Take(collidedObj, this.ray.origin);
                        }
                    }
                }
            }
        }
    }
}