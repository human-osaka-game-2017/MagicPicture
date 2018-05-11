using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmManager : MonoBehaviour {

    [SerializeField] private int kMaxFilm;
    [SerializeField] private int kMaxPhantom;
    [SerializeField] private float kCoordinateUnit;
    [SerializeField] private float kPhantomDistance;
    [SerializeField] private float kScaleThreshold_StoM_per;
    [SerializeField] private float kScaleThreshold_MtoL_per;
    [SerializeField] private float kScaleRatio_S;
    [SerializeField] private float kScaleRatio_M;
    [SerializeField] private float kScaleRatio_L;

    public struct Film
    {
        public GameObject obj;
        public float offset_y;
        public float rot_y;
        public float scale;

        public void ResetPos(int filmIndex)
        {
            obj.transform.position = new Vector3(
                100.0f * filmIndex,
                5000.0f,
                0.0f);
        }
    }

    private float maxDistance = 0;
    public  float MaxDistance
    {
        set { maxDistance = value; }
    }

    private Film[] films          = null;
    private GameObject[] phantoms = null;
    private GameObject silhouette = null;
    private GameObject player     = null;
    private GameObject magicame   = null;
    private int currentFilmNum    = 0;
    private int prevFilmNum       = 0;
    private bool isPhantomMode    = false;

    void Start()
    {
        this.films    = new Film[kMaxFilm];
        this.phantoms = new GameObject[kMaxPhantom];
        player = GameObject.Find("Player");
        magicame = player.transform.FindChild("FPSCamera").gameObject;
    }

    void Update()
    {
        //フィルム選択の更新
        UpdateCurrentFilmNum();

        //PhantomModeの変更
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.isPhantomMode = !this.isPhantomMode;
        }

        if (this.isPhantomMode)
        {
            //シルエットの更新
            silhouette = films[currentFilmNum].obj ;
            if (silhouette != null)
            {
                {
                    silhouette.GetComponent<Collider>().isTrigger = true;

                    //各定点に移動
                    Vector3 pos =
                    magicame.transform.position + (magicame.transform.forward.normalized * kPhantomDistance);
                    pos.x =  ((int)(pos.x / kCoordinateUnit)) * kCoordinateUnit;
                    pos.y -= this.films[this.currentFilmNum].offset_y;
                    pos.z =  ((int)(pos.z / kCoordinateUnit)) * kCoordinateUnit;

                    silhouette.transform.position = pos;
                }

                if (prevFilmNum != currentFilmNum)
                {
                    //前の状態に戻す
                    this.films[prevFilmNum].ResetPos(prevFilmNum);
                    //マテリアルうんぬんかんぬん

                    //更新
                    //マテリアルうんぬんかんぬん
                }
            }

            prevFilmNum = currentFilmNum;
        }

        //現像
        if (Input.GetKeyDown(KeyCode.F) && this.isPhantomMode)
        {
            if(silhouette.GetComponent<ObjectAttribute>().CanPhantom) DevelopPhantom();
        }
    }

    private void DevelopPhantom()
    {
        AddPhantom(this.films[this.currentFilmNum].obj);

        //追加時の各設定
        this.phantoms[0].GetComponent<ObjectAttribute>().Taken();
    }

    //@param 撮影のray
    public void SetFilm(/*const*/ RaycastHit filmingObj ,Vector3 rayOrigin)
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
                else if(distance <= maxDistance * kScaleThreshold_MtoL_per * 0.01)
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
                //todo 分割
                float dx = filmingObj.collider.gameObject.transform.position.x - rayOrigin.x;
                float dy = filmingObj.collider.gameObject.transform.position.y - rayOrigin.y;
                this.films[this.currentFilmNum].rot_y = Mathf.Atan2(dy, dx);
            }
        }

        //生成
        {
            this.films[this.currentFilmNum].obj = Instantiate(filmingObj.collider.gameObject);

            this.films[this.currentFilmNum].ResetPos(currentFilmNum);

            this.films[this.currentFilmNum].obj.transform.localScale *= this.films[this.currentFilmNum].scale;

            this.films[this.currentFilmNum].obj.transform.rotation = Quaternion.Euler(new Vector3(
                0.0f,
                this.films[this.currentFilmNum].rot_y,
                0.0f));
        }
    }

    private void UpdateCurrentFilmNum()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (this.currentFilmNum == 0)
            {
                this.currentFilmNum = this.kMaxFilm;
            }
            else
            {
                --this.currentFilmNum;
            }
            Debug.Log(this.currentFilmNum);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (this.currentFilmNum == this.kMaxFilm)
            {
                this.currentFilmNum = 0;
            }
            else
            {
                ++this.currentFilmNum;
            }
            Debug.Log(this.currentFilmNum);
        }
    }

    //@param film
    //phantomの要素番号0番に追加
    private void AddPhantom(GameObject film)
    {
        GameObject phantom = Instantiate(film);
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

            Destroy(next);//nullでもエラー起きない
        }
        else
        {
            this.phantoms[0] = phantom;
        }
    }
}
