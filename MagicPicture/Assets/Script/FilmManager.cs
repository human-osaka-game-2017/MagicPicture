using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmManager : MonoBehaviour {

    [SerializeField] private int kMaxFilm;
    [SerializeField] private int kMaxPhantom;
    [SerializeField] private float kCoordinateUnit;
    [SerializeField] private float kPhantomDistance;

    private GameObject[] films    = null;
    private GameObject[] phantoms = null;
    private GameObject silhouette = null;
    private GameObject player     = null;
    private int currentFilmNum  = 0;
    private int prevFilmNum     = 0;
    private bool isPhantomMode      = false;

    //public void EntryModel(string objectName)
    //{
    //    if (!this.models.ContainsKey(objectName))
    //    {
    //        this.models.Add()
    //    }
    //}

    void Start()
    {
        this.films = new GameObject[kMaxFilm];
        this.phantoms = new GameObject[kMaxPhantom];
        prevFilmNum = kMaxFilm - 1;
        player = GameObject.Find("Player");
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
            silhouette = films[currentFilmNum];
            if (silhouette != null)
            {
                {
                    silhouette.GetComponent<Collider>().isTrigger = true;

                    Vector3 pos =
                    player.transform.position + (player.transform.forward.normalized * kPhantomDistance);
                    //各定点に移動、yは撮影時の位置
                    pos.x = ((int)(pos.x / kCoordinateUnit)) * kCoordinateUnit;
                    pos.y = this.films[this.currentFilmNum].transform.position.y;
                    pos.z = ((int)(pos.z / kCoordinateUnit)) * kCoordinateUnit;

                    silhouette.transform.position = pos;
                }

                if (prevFilmNum != currentFilmNum)
                {
                    //シルエットを前の状態に戻す(isTriggerの解除)
                    //座標うんぬんかんぬん
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
            if(silhouette.GetComponent<ObjectAttribute>().CanPhantom)
            DevelopPhantom();
        }
    }

    private void DevelopPhantom()
    {
        AddPhantom(this.films[this.currentFilmNum]);

        //追加時の各設定
        this.phantoms[0].GetComponent<ObjectAttribute>().Taken();
    }

    //@param 撮影したオブジェクト
    public void SetFilm(/*const*/ GameObject filmingObj)
    {
        if (this.films[this.currentFilmNum] != null)
        {
            Destroy(this.films[this.currentFilmNum]);
            //this.films[this.currentFilmNum] = null;
        }

        Vector3 pos = Vector3.zero;
        //todo 八等分 座標移動
        this.films[this.currentFilmNum] = Instantiate(
            filmingObj,
            pos,
            filmingObj.transform.rotation);

        //this.films[this.currentFilmNum].SetActive(false);
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
