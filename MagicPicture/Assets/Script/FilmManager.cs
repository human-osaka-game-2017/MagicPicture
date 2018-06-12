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
    [SerializeField, Range(0.0f, 1.0f)] private float alphaSilhouette;
    [SerializeField, Range(0.0f, 1.0f)] private float alphaPhantom;

    private const int filmSpace_x = 100;
    private const int filmSpace_y = 5000;

    public class Film
    {
        public Film(int index)
        {
            SetIndex(index);
        }
        
        public void SetIndex(int index)
        {
            farawaySpace = new Vector3(
                filmSpace_x * index,
                filmSpace_y,
                0.0f);
        }

        //private GameObject obj { get; set; }
        public GameObject obj;
        public RawImage image;
        public float offset_y;
        public float rot_y;
        public float scale;

        private int index;
        private Vector3 farawaySpace;

        public void ResetPos()
        {
            obj.transform.position = farawaySpace;
        }
    }

    //Magicameにセットしてもらう
    private float maxDistance = 0;
    public  float MaxDistance
    {
        set { maxDistance = value; }
    }

    private Film[]          films           = null;
    private GameObject[]    phantoms        = null;
    private Film            silhouette      = null;
    private GameObject      player          = null;
    private GameObject      magicame        = null;
    private Camera          photoUICamera   = null;
    private int currentFilmNum    = 0;
    private int prevFilmNum       = 0;
    private bool isPhantomMode    = false;

    void Start()
    {
        //RawImage img = new RawImage();
        this.films = new Film[this.kMaxFilm];
        for(int i = 0; i < this.kMaxFilm ; ++i){
            this.films[i] = new Film(i);
        }

        this.phantoms = new GameObject[kMaxPhantom];

        player        = GameObject.Find("Player");
        magicame      = player.transform.Find("FPSCamera").gameObject;
        photoUICamera = GameObject.Find("PhotoUICamera").GetComponent<Camera>();

        //ほかに書き方あるかも
        for (int i = 0; i < this.kMaxFilm; ++i)
        {
            string path = "Canvas/photo/back (" + (i + 1).ToString() + ")/Picture (" + (i + 1).ToString() + ")";
            films[i].image = this.transform.Find(path).gameObject.GetComponent<RawImage>();
        }

        silhouette = films[currentFilmNum];
    }

    void Update()
    {
        //フィルム選択の更新
        UpdateCurrentFilmNum();

        //選択フィルム変更
        if (this.prevFilmNum != this.currentFilmNum)
        {
            SoundManager.GetInstance().Play("SE_FilmToSwitch", SoundManager.PLAYER_TYPE.NONLOOP, true);

            int influenceNum = this.currentFilmNum - this.prevFilmNum;
            int direction = influenceNum / Math.Abs(influenceNum);

            float halfSize = this.films[this.currentFilmNum].image.GetComponentInParent<RectTransform>().sizeDelta.x / 2;

            //縮小、移動
            this.films[this.prevFilmNum].image.gameObject.transform.parent.localScale = Vector3.one;
            this.films[this.prevFilmNum].image.transform.parent.position += new Vector3(halfSize * -direction, 0, 0);

            //移動
            for (int i = this.prevFilmNum + direction ; i != currentFilmNum; i += direction)
            {
                this.films[i].image.transform.parent.position += new Vector3(this.films[i].image.GetComponentInParent<RectTransform>().sizeDelta.x * -direction, 0, 0);
            }

            //拡大、移動
            this.films[this.currentFilmNum].image.gameObject.transform.parent.localScale = new Vector3(2.0f, 2.0f, 1.0f);
            this.films[this.currentFilmNum].image.transform.parent.position += new Vector3(halfSize * -direction, 0, 0);

            ChangeSilhouette(films[currentFilmNum]);
        }

        if (films[currentFilmNum].obj != null)
        {
            //PhantomModeの変更
            if (Input.GetButtonDown("ForSilhouetteMode"))
            {
                this.isPhantomMode = !this.isPhantomMode;
                
                ChangeSilhouette(films[currentFilmNum]);
            }

            if (this.isPhantomMode)
            {
                UpdateSilhouette();
            }

            //写真、オブジェクトの回転
            if (AxisStateManager.GetInstance().GetAxisDown("ForRotatePicture") != 0)
            {
                films[currentFilmNum].image.transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxisRaw("ForRotatePicture") * 90.0f));
                Vector3 rot = Vector3.zero;
                rot.x = Mathf.Cos(films[currentFilmNum].rot_y * Mathf.Deg2Rad);
                rot.y = 0.0f;
                rot.z = Mathf.Sin(films[currentFilmNum].rot_y * Mathf.Deg2Rad);
                films[currentFilmNum].obj.transform.Rotate(rot.normalized * 90.0f * Input.GetAxisRaw("ForRotatePicture"), Space.Self);
            }

            //現像
            if ((Input.GetButtonDown("ForDevelopPhantom") || AxisStateManager.GetInstance().GetAxisDown("ForDevelopPhantom") == 1)
                && this.isPhantomMode)
            {
                if (silhouette.obj.GetComponent<ObjectAttribute>().CanPhantom) DevelopPhantom();
            }
        }

        prevFilmNum = currentFilmNum;
    }

    private void DevelopPhantom()
    {
        SoundManager.GetInstance().Play("SE_PhantomPutOut", SoundManager.PLAYER_TYPE.NONLOOP, true);

        EffectManager.GetInstance().PopUp("appear", this.films[this.currentFilmNum].obj.transform.position);

        AddPhantom(this.films[this.currentFilmNum].obj);

        //追加時の各設定
        this.phantoms[0].GetComponent<ObjectAttribute>().Taken();
        this.phantoms[0].transform.parent = null;
        this.phantoms[0].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, alphaPhantom));
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

            this.films[this.currentFilmNum].ResetPos();

            this.films[this.currentFilmNum].obj.transform.localScale *= this.films[this.currentFilmNum].scale;

            this.films[this.currentFilmNum].obj.transform.SetParent(this.player.transform, true);
        }

        //スクショ
        {
            //カメラの移動
            //photo用カメラ撮影位置
            Vector3 pos = filmingObj.point - rayOrigin;
            pos.Scale(new Vector3(1, 0, 1));
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

        this.films[this.currentFilmNum].image.texture = photo;
        this.films[this.currentFilmNum].image.color = Color.white;
        Material material = this.films[this.currentFilmNum].obj.GetComponent<Renderer>().material;
        BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
        material.SetColor("_Color", new Color(1, 1, 1, alphaSilhouette));
    }

    //@param 変更するオブジェクト
    private void ChangeSilhouette(Film film)
    {
        if (silhouette.obj != null)
        {
            silhouette.ResetPos();
            silhouette.obj.GetComponent<Collider>().isTrigger = false;
        }

        //変更
        silhouette = film;

        if (silhouette.obj != null)
        {
            silhouette.obj.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            this.isPhantomMode = false;
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

        silhouette.obj.transform.position = pos;
        silhouette.obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void UpdateCurrentFilmNum()
    {
        Func<int, int> standardizationFilmNum = nextFilmNum => (nextFilmNum + this.kMaxFilm) % this.kMaxFilm;
        
        if (AxisStateManager.GetInstance().GetAxisDown("HorizontalForChangeFilm") == 1.0f)
        {
            this.currentFilmNum = standardizationFilmNum(--currentFilmNum);
        }
        else if (AxisStateManager.GetInstance().GetAxisDown("HorizontalForChangeFilm") == -1.0f)
        {
            this.currentFilmNum = standardizationFilmNum(++currentFilmNum);
        }
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
