using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FilmManager : MonoBehaviour {

    [SerializeField] private GameObject guideEffect;
    [SerializeField] private int maxFilm;
    [SerializeField] private int maxPhantom;
    [SerializeField] private float coordinateUnit;
    [SerializeField] private float phantomDistance;
    [SerializeField, Range(0.0f, 100.0f)] private float scaleThresholdStoMOfPer;
    [SerializeField, Range(0.0f, 100.0f)] private float scaleThresholdMtoLOfPer;
    [SerializeField] private float scaleRatioS;
    [SerializeField] private float scaleRatioM;
    [SerializeField] private float scaleRatioL;
    [SerializeField] private float photoCameraRange;
    [SerializeField, Range(0.0f, 1.0f)] private float alphaValOfSilhouette;
    [SerializeField, Range(0.0f, 1.0f)] private float alphaValOfPhantom;

    private const int filmSpaceOfX = 100;
    private const int filmSpaceOfY = 5000;

    private class Film
    {
        public Film(int index)
        {
            SetIndex(index);
        }
        
        public void SetIndex(int index)
        {
            farawaySpace = new Vector3(
                filmSpaceOfX * index,
                filmSpaceOfY,
                0.0f);
        }

        public GameObject Obj { get; set; }
        public RawImage Image { get; set; }
        public Vector3 Axis { get; set; }
        public float OffsetToY { get; set; }
        public float Scale { get; set; }
        private Vector3 farawaySpace;

        public void ResetPos()
        {
            Obj.transform.position = farawaySpace;
        }
    }

    //Magicameにセットしてもらう
    private float maxDistance = 0;
    public  float MaxDistance
    {
        set { maxDistance = value; }
    }

    private Film[] films;
    private Film CurrentFilm
    {
        get { return films[currentFilmNum]; }
    }
    private GameObject[] phantoms;
    private Film silhouette;
    private GameObject player;
    private GameObject magicame;
    private Camera photoUICamera;
    private int currentFilmNum;
    private int prevFilmNum;
    private bool isSilhouetteMode;

    void Start()
    {
        this.films = new Film[this.maxFilm];
        for(int i = 0; i < this.maxFilm ; ++i){
            this.films[i] = new Film(i);
        }

        this.phantoms = new GameObject[maxPhantom];

        player        = GameObject.Find("Player");
        magicame      = player.transform.Find("FPSCamera").gameObject;
        photoUICamera = GameObject.Find("PhotoUICamera").GetComponent<Camera>();

        //ほかに書き方あるかも
        for (int i = 0; i < this.maxFilm; ++i)
        {
            string path = "Canvas/photo/back (" + (i + 1).ToString() + ")/Picture (" + (i + 1).ToString() + ")";
            films[i].Image = this.transform.Find(path).gameObject.GetComponent<RawImage>();
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

            float halfSize = this.films[this.currentFilmNum].Image.GetComponentInParent<RectTransform>().sizeDelta.x / 2;

            //縮小、移動
            this.films[this.prevFilmNum].Image.transform.parent.localScale = Vector3.one;
            this.films[this.prevFilmNum].Image.transform.parent.localPosition += new Vector3(halfSize * -direction, 0, 0);

            //移動
            for (int i = this.prevFilmNum + direction ; i != currentFilmNum; i += direction)
            {
                this.films[i].Image.transform.parent.localPosition += new Vector3(this.films[i].Image.GetComponentInParent<RectTransform>().sizeDelta.x * -direction, 0, 0);
            }

            //拡大、移動
            this.films[this.currentFilmNum].Image.transform.parent.localScale = new Vector3(2.0f, 2.0f, 1.0f);
            this.films[this.currentFilmNum].Image.transform.parent.localPosition += new Vector3(halfSize * -direction, 0, 0);

            ChangeSilhouette(films[currentFilmNum]);
        }

        //phantom全消し
        if (Input.GetButtonDown("ForDeleteAllPhantom"))
        {
            foreach(var phantom in this.phantoms)
            {
                DeletePhantom(phantom);
            }
        }

        if (films[currentFilmNum].Obj != null)
        {
            //SilhouetteModeの変更
            if (Input.GetButtonDown("ForSilhouetteMode"))
            {
                this.isSilhouetteMode = !this.isSilhouetteMode;
                this.guideEffect.SetActive(!this.guideEffect.activeSelf);
                ChangeSilhouette(films[currentFilmNum]);
            }

            if (this.isSilhouetteMode)
            {
                UpdateSilhouette();
            }

            //写真、オブジェクトの回転
            if (AxisStateManager.GetInstance().GetAxisDown("ForRotatePicture") != 0)
            {
                films[currentFilmNum].Image.transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxisRaw("ForRotatePicture") * 90.0f));
                films[currentFilmNum].Obj.transform.Rotate(this.films[currentFilmNum].Axis * Input.GetAxisRaw("ForRotatePicture"), 90.0f, Space.Self);
            }

            //現像
            if ((Input.GetButtonDown("ForDevelopPhantom") || AxisStateManager.GetInstance().GetAxisDown("ForDevelopPhantom") == 1)
                && this.isSilhouetteMode)
            {
                if (silhouette.Obj.GetComponent<ObjectAttribute>().CanPhantom) DevelopPhantom();
            }
        }

        prevFilmNum = currentFilmNum;
    }

    private void DevelopPhantom()
    {
        SoundManager.GetInstance().Play("SE_PhantomPutOut", SoundManager.PLAYER_TYPE.NONLOOP, true);

        EffectManager.GetInstance().PopUp("appear", this.films[this.currentFilmNum].Obj.transform.position);

        AddPhantom(this.films[this.currentFilmNum].Obj);

        //追加時の各設定
        this.phantoms[0].transform.SetParent(null, true);
        this.phantoms[0].GetComponent<Collider>().isTrigger = false;
        this.phantoms[0].GetComponent<Rigidbody>().useGravity = true;
        this.phantoms[0].layer = LayerMask.NameToLayer("Default");
        this.phantoms[0].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, alphaValOfPhantom));
        this.phantoms[0].AddComponent<NavMeshObstacle>();
        this.phantoms[0].GetComponent<NavMeshObstacle>().carving = true;
    }

    //@param 撮影のray
    //@param 撮影時のrayのスタート位置
    public void Take(RaycastHit filmingObj ,Vector3 rayOrigin)
    {
        if (this.films[this.currentFilmNum].Obj != null)
        {
            Destroy(this.films[this.currentFilmNum].Obj);
        }

        Vector3 vector3;

        //offset,scale,rotの設定
        {
            //offset
            this.films[this.currentFilmNum].OffsetToY = filmingObj.point.y - filmingObj.collider.gameObject.transform.position.y;

            //scale
            {
                float distance = filmingObj.distance;
                if (distance <= maxDistance * scaleThresholdStoMOfPer * 0.01)
                {
                    this.films[this.currentFilmNum].Scale = scaleRatioL;
                }
                else if (distance <= maxDistance * scaleThresholdMtoLOfPer * 0.01)
                {
                    this.films[this.currentFilmNum].Scale = scaleRatioM;
                }
                else
                {
                    this.films[this.currentFilmNum].Scale = scaleRatioS;
                }
            }

            //angle
            {
                Vector2 vec =  new Vector2(rayOrigin.x, rayOrigin.z) - new Vector2(filmingObj.collider.gameObject.transform.position.x, filmingObj.collider.gameObject.transform.position.z);
                float deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                float unit_deg = 45.0f;//8等分
                deg = Mathf.Floor((deg + (unit_deg / 2)) / unit_deg) * unit_deg;
                vector3 = Quaternion.Euler(0.0f, -deg, 0.0f) * Vector3.right;
            }
        }

        //生成
        {
            this.films[this.currentFilmNum].Obj = Instantiate(filmingObj.collider.gameObject);

            this.films[this.currentFilmNum].ResetPos();

            this.films[this.currentFilmNum].Obj.transform.localScale *= this.films[this.currentFilmNum].Scale;

            this.films[this.currentFilmNum].Obj.GetComponent<Collider>().isTrigger = true;
            this.films[this.currentFilmNum].Obj.GetComponent<Rigidbody>().useGravity = false;
            this.films[this.currentFilmNum].Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.films[this.currentFilmNum].Obj.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        //スクショ
        {
            //カメラの移動
            //photo用カメラ撮影位置
            Vector3 pos = this.films[currentFilmNum].Obj.transform.position;
            pos += vector3 * photoCameraRange;
            photoUICamera.transform.position = pos;
            photoUICamera.transform.LookAt(this.films[currentFilmNum].Obj.transform.position);

            StartCoroutine("CreatePhoto", vector3);
        }
    }

    IEnumerator CreatePhoto(Vector3 vec)
    {
        yield return new WaitForEndOfFrame();

        int width = (int)films[this.currentFilmNum].Image.rectTransform.rect.width;
        int height = (int)films[this.currentFilmNum].Image.rectTransform.rect.height;

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

        this.films[this.currentFilmNum].Image.texture = photo;
        this.films[this.currentFilmNum].Image.color = Color.white;
        Material material = this.films[this.currentFilmNum].Obj.GetComponent<Renderer>().material;
        BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
        material.SetColor("_Color", new Color(1, 1, 1, alphaValOfSilhouette));
        this.films[this.currentFilmNum].Obj.transform.SetParent(this.player.transform, true);
        this.films[this.currentFilmNum].Axis = Quaternion.Inverse(Quaternion.Euler(this.films[this.currentFilmNum].Obj.transform.eulerAngles)) * vec;
    }

    private void ChangeSilhouette(Film film)
    {
        if (silhouette.Obj != null)
        {
            silhouette.ResetPos();
        }

        //変更
        silhouette = film;

        if (silhouette.Obj == null)
        {
            this.isSilhouetteMode = false;
            this.guideEffect.SetActive(false);
        }
    }

    private void UpdateSilhouette()
    {
        //各定点に移動
        Vector3 pos =
        magicame.transform.position + (magicame.transform.forward.normalized * phantomDistance);
        pos.x = ((int)(pos.x / coordinateUnit)) * coordinateUnit;
        pos.y -= this.films[this.currentFilmNum].OffsetToY;
        pos.z = ((int)(pos.z / coordinateUnit)) * coordinateUnit;

        silhouette.Obj.transform.position = pos;
        guideEffect.transform.position = pos;
    }

    private void UpdateCurrentFilmNum()
    {
        Func<int, int> standardizationFilmNum = nextFilmNum => (nextFilmNum + this.maxFilm) % this.maxFilm;
        
        if (AxisStateManager.GetInstance().GetAxisDown("HorizontalForChangeFilm") == 1.0f)
        {
            this.currentFilmNum = standardizationFilmNum(--currentFilmNum);
        }
        else if (AxisStateManager.GetInstance().GetAxisDown("HorizontalForChangeFilm") == -1.0f)
        {
            this.currentFilmNum = standardizationFilmNum(++currentFilmNum);
        }
    }

    public void DeletePhantom(GameObject phantom)
    {
        if (phantom == null) return;

        //エフェクト
        EffectManager.GetInstance().PopUp("disappear", phantom.transform.position);
        //音声

        Destroy(phantom);
    }

    private void AddPhantom(GameObject film)
    {
        GameObject phantom = Instantiate(
            film,
            film.transform.position,
            film.transform.rotation);

        phantom.GetComponent<ObjectAttribute>().Taken(); ;

        if (this.phantoms[0] != null)
        {
            GameObject next = phantom; //次に代入するやつ
            GameObject tmp; //一時保管

            int count;
            for (count = 0; count < maxPhantom; ++count)
            {
                tmp = this.phantoms[count];
                this.phantoms[count] = next;
                next = tmp;
                if (tmp == null)
                {
                    break;
                }
            }

            DeletePhantom(next);
        }
        else
        {
            this.phantoms[0] = phantom;
        }
    }
}
