using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour
{
    [SerializeField] private float kMinDistance;
    [SerializeField] private float kMaxDistance;

    private GameObject player = null;
    private FilmManager filmManager = null;
    private PermeationImage img = null;
    private Vector3 rotSpeed;

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
        GameObject.Find("FilmManager").GetComponent<FilmManager>().MaxDistance = kMaxDistance - kMinDistance;
    }

    void Update()
    {
        this.transform.Rotate(rotSpeed * Input.GetAxis("VerticalForView") * Time.deltaTime);

        // カメラ上下回転リセット
        //if (Input.GetButtonDown("ForResetCameraView"))
        //{
        //    Init();
        //}

        //撮影
        // Rayが衝突したコライダーの情報を得る
        RaycastHit hitObj;
        if (Physics.Raycast(
            this.transform.position+(this.transform.forward * this.kMinDistance),
            this.transform.forward,
            out hitObj,
            this.kMaxDistance - this.kMinDistance))
        {
            //シャッターを切られた
            if (Input.GetButtonDown("ForTakePicture"))
            {
                img.Init();

                if (hitObj.collider.GetComponent<ObjectAttribute>().CanTake)
                {
                    SoundManager.GetInstance().Play("SE_Shutter", SoundManager.PLAYER_TYPE.NONLOOP, true);

                    filmManager.Take(hitObj, this.transform.position);
                }
                else if (hitObj.collider.GetComponent<ObjectAttribute>().IsTakenObj)
                {
                    filmManager.DeletePhantom(hitObj.collider.gameObject);
                }
            }
        }
    }
}