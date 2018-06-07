using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class FilmManager : MonoBehaviour {

    [SerializeField] private int   kMaxFilm;
    [SerializeField] private int   kMaxPhantom;
    [SerializeField] private float kCoordinateUnit;
    [SerializeField] private float kPhantomDistance;
    [SerializeField] private float kScaleThreshold_StoM_per;
    [SerializeField] private float kScaleThreshold_MtoL_per;
    [SerializeField] private float kScaleRatio_S;
    [SerializeField] private float kScaleRatio_M;
    [SerializeField] private float kScaleRatio_L;
    [SerializeField] private float photoCameraRange;

    private const int filmSpace_x = 100;
    private const int filmSpace_y = 5000;

    public struct Film
    {
        //private GameObject obj { get; set; }
        public GameObject obj;
        public RawImage image;
        public float offset_y;
        public float rot_y;
        public float scale;

        public void ResetPos(int filmIndex)
        {
            obj.transform.position = new Vector3(
                filmSpace_x * filmIndex,
                filmSpace_y,
                0.0f);
        }
    }

    //Magicameにセットしてもらう
    private float maxDistance = 0;
    public  float MaxDistance
    {
        set { maxDistance = value; }
    }

    private Film[]       films       = null;
    private GameObject[] phantoms    = null;
    private GameObject   silhouette  = null;
    private GameObject   player      = null;
    private GameObject   magicame    = null;
    private Camera       photoUICamera = null;
    private int currentFilmNum    = 0;
    private int prevFilmNum       = 0;
    private bool isPhantomMode    = false;

    void Start()
    {
        //RawImage img = new RawImage();
        this.films    = new Film[kMaxFilm];
        this.phantoms = new GameObject[kMaxPhantom];

        player        = GameObject.Find("Player");
        magicame      = player.transform.Find("FPSCamera").gameObject;
        photoUICamera = GameObject.Find("PhotoUICamera").GetComponent<Camera>();

        //ほかに書き方あるかも
        RawImage[] images;
        images = this.transform.Find("Canvas/photo").gameObject.GetComponentsInChildren<RawImage>();
        for (int i = 0; i < images.Length; i++)
        {
            films[i].image = images[i];
        }
    }

    void Update()
    {
        //フィルム選択の更新
        UpdateCurrentFilmNum();

        if (films[currentFilmNum].obj != null)
        {
            //PhantomModeの変更
            if (Input.GetButtonDown("ForSilhouetteMode"))
            {
                this.isPhantomMode = !this.isPhantomMode;
                if (!this.isPhantomMode)
                {
                    films[currentFilmNum].ResetPos(currentFilmNum);
                }
                else
                {
                    ChangeSilhouette(films[currentFilmNum].obj);
                }
            }

            if (this.isPhantomMode)
            {
                if (prevFilmNum != currentFilmNum)
                {
                    ChangeSilhouette(films[currentFilmNum].obj);
                }

                UpdateSilhouette();

                prevFilmNum = currentFilmNum;
            }

            //写真、オブジェクトの回転
            if (Input.GetButtonDown("ForRotatePicture"))
            {
                films[currentFilmNum].image.transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxisRaw("ForRotatePicture") * 90.0f));
                Vector3 rot = Vector3.zero;
                rot.x = Mathf.Cos(films[currentFilmNum].rot_y * Mathf.Deg2Rad);
                rot.y = 0.0f;
                rot.z = Mathf.Sin(films[currentFilmNum].rot_y * Mathf.Deg2Rad);
                films[currentFilmNum].obj.transform.Rotate(rot.normalized * 90.0f * Input.GetAxisRaw("ForRotatePicture"), Space.Self);
            }

            //現像
            if (Input.GetButtonDown("ForDevelopPhantom") && this.isPhantomMode)
            {
                if (silhouette.GetComponent<ObjectAttribute>().CanPhantom) DevelopPhantom();
            }
        }
    }

    private void DevelopPhantom()
    {
        AddPhantom(this.films[this.currentFilmNum].obj);

        //追加時の各設定
        this.phantoms[0].GetComponent<ObjectAttribute>().Taken();
        this.phantoms[0].transform.parent = null;
    }

    //@param 撮影のray
    //@param 撮影時のrayのスタート位置
    public void Take(RaycastHit filmingObj ,Vector3 rayOrigin)
    {
        if (this.films[this.currentFilmNum].obj != null)
        {
            Destroy(this.films[this.currentFilmNum].obj);
        }

        //offset,scale,rotの設定
        {
            //offset
            this.films[this.currentFilmNum].offset_y = filmingObj.point.y - filmingObj.collider.gameObject.transform.position.y;

            //scale
            {
                float distance = filmingObj.distance;
                if (distance <= maxDistance * kScaleThreshold_StoM_per * 0.01)
                {
                    this.films[this.currentFilmNum].scale = kScaleRatio_L;
                }
                else if (distance <= maxDistance * kScaleThreshold_MtoL_per * 0.01)
                {
                    this.films[this.currentFilmNum].scale = kScaleRatio_M;
                }
                else
                {
                    this.films[this.currentFilmNum].scale = kScaleRatio_S;
                }
            }

            //rotation
            {
                //もう少しいい書き方あるかも
                float dx = rayOrigin.x - filmingObj.collider.gameObject.transform.position.x;
                float dz = rayOrigin.z - filmingObj.collider.gameObject.transform.position.z;
                float deg = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
                if (deg <= 22.5f || 338.5f < deg)
                {
                    this.films[this.currentFilmNum].rot_y = 0.0f;
                }
                else if (deg <= 67.5f)
                {
                    this.films[this.currentFilmNum].rot_y = 45.0f;
                }
                else if (deg <= 112.5)
                {
                    this.films[this.currentFilmNum].rot_y = 90.0f;
                }
                else if (deg <= 157.5)
                {
                    this.films[this.currentFilmNum].rot_y = 135.0f;
                }
                else if (deg <= 202.5f)
                {
                    this.films[this.currentFilmNum].rot_y = 180.0f;
                }
                else if (deg <= 247.5f)
                {
                    this.films[this.currentFilmNum].rot_y = 225.0f;
                }
                else if (deg <= 292.5f)
                {
                    this.films[this.currentFilmNum].rot_y = 270.0f;
                }
                else if (deg <= 337.5f)
                {
                    this.films[this.currentFilmNum].rot_y = 315.0f;
                }
            }
        }

        //生成
        {
            this.films[this.currentFilmNum].obj = Instantiate(filmingObj.collider.gameObject);

            this.films[this.currentFilmNum].ResetPos(currentFilmNum);

            this.films[this.currentFilmNum].obj.transform.localScale *= this.films[this.currentFilmNum].scale;

            this.films[this.currentFilmNum].obj.transform.SetParent(this.player.transform, true);
        }

        //スクショ
        {
            //todo 別スクリプトへ
            //カメラの移動
            //photo用カメラ撮影位置
            Vector3 pos = filmingObj.point - rayOrigin;

            pos.Normalize();
            pos *= photoCameraRange;
            pos += this.films[currentFilmNum].obj.transform.position;
            photoUICamera.transform.position = pos;
            photoUICamera.transform.LookAt(this.films[currentFilmNum].obj.transform.position);

            StartCoroutine("CreatePhoto");
        }
    }

    IEnumerator CreatePhoto()
    {
        yield return new WaitForEndOfFrame();

        int width = (int)films[this.currentFilmNum].image.rectTransform.rect.width;
        int height = (int)films[this.currentFilmNum].image.rectTransform.rect.height;

        // アクティブなレンダーテクスチャをキャッシュしておく
        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(width, height, 24);

        // アクティブなレンダーテクスチャを一時的にTargetに変更する
        RenderTexture.active = renderTexture;

        photoUICamera.targetTexture = renderTexture;

        photoUICamera.Render();

        Texture2D photo = new Texture2D(width, height);
        photo.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        photo.Apply();

        films[this.currentFilmNum].image.texture = photo;
    }

    //@param 変更するオブジェクト
    private void ChangeSilhouette(GameObject obj)
    {
        if (silhouette != null)
        {
            //todo シルエットをリセットできるように書く
            films[currentFilmNum].ResetPos(currentFilmNum);
            silhouette.GetComponent<Collider>().isTrigger = false;
        }

        //変更
        silhouette = obj;

        if (silhouette != null)
        {
            silhouette.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void UpdateSilhouette()
    {
        //各定点に移動
        Vector3 pos =
        magicame.transform.position + (magicame.transform.forward.normalized * kPhantomDistance);
        pos.x = ((int)(pos.x / kCoordinateUnit)) * kCoordinateUnit;
        pos.y -= this.films[this.currentFilmNum].offset_y;
        pos.z = ((int)(pos.z / kCoordinateUnit)) * kCoordinateUnit;

        silhouette.transform.position = pos;
    }

    private void UpdateCurrentFilmNum()
    {
        Func<int, int> standardizationFilmNum = nextFilmNum => (nextFilmNum + this.kMaxFilm) % this.kMaxFilm;

        if (Input.GetButtonDown("HorizontalForChangeFilm"))
        {
            if (Input.GetAxis("HorizontalForChangeFilm") < 0.0f)
            {
                this.currentFilmNum = standardizationFilmNum(--currentFilmNum);
            }
            else if (Input.GetAxis("HorizontalForChangeFilm") > 0.0f)
            {
                this.currentFilmNum = standardizationFilmNum(++currentFilmNum);
            }
        }
        Debug.Log(this.currentFilmNum);
    }

    //phantomの要素番号0番に追加
    //@param film
    private void AddPhantom(GameObject film)
    {
        GameObject phantom = Instantiate(
            film,
            film.transform.position,
            film.transform.rotation);

        phantom.GetComponent<Collider>().isTrigger = false;

        if (this.phantoms[0] != null)
        {
            GameObject next = phantom; //次に代入するやつ
            GameObject tmp; //一時保管

            int count;
            for (count = 0; count < kMaxPhantom; ++count)
            {
                tmp = this.phantoms[count];
                this.phantoms[count] = next;
                next = tmp;
                if (tmp == null)
                {
                    break;
                }
            }

            Destroy(next);//内部でnullチェックが行われているためnullでもエラー起きない
        }
        else
        {
            this.phantoms[0] = phantom;
        }
    }
}
