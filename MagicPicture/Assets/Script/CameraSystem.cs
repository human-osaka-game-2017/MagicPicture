using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CameraSystem: MonoBehaviour {

    [SerializeField] private GameObject FPSCamera;
    [SerializeField] private GameObject TPSCamera;
    [SerializeField] private int kMaxFilm;
    [SerializeField] private int kMaxPhantom;
    [SerializeField] private float kCoordinateUnit;
    [SerializeField] private float kPhantomDistance;

    private GameObject[] films;
    private GameObject[] phantoms;
    private int currentFilmNum = 0;
    public int CurrentFilmNum
    {
        get { return this.currentFilmNum; }
    }

    private bool isFPSMode = false;
    public bool IsFPSMode
    {
        get { return this.isFPSMode; }
        private set { this.isFPSMode = value; }
    }

    void Start ()
    {
        this.films      = new GameObject[kMaxFilm];
        this.phantoms   = new GameObject[kMaxPhantom];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ChangeFTMode();

        //フィルム選択の更新
        UpdateCurrentFilmNum();

        //現像
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 pos =
            this.transform.position + (this.transform.forward.normalized * kPhantomDistance);
            pos.x = ((int)(pos.x / kCoordinateUnit)) * kCoordinateUnit;
            pos.y = this.films[this.currentFilmNum].transform.position.y;
            pos.z = ((int)(pos.z / kCoordinateUnit)) * kCoordinateUnit;

            AddPhantom(Instantiate(
                this.films[this.currentFilmNum],
                pos,
                this.films[this.currentFilmNum].transform.localRotation));

            this.phantoms[0].GetComponent<ObjectAttribute>().Taken();
            this.phantoms[0].SetActive(true);
        }
    }

    //param 撮影したオブジェクトのコピー
    public void SetFilm(GameObject filmingObj)
    {
        if(this.films[this.currentFilmNum] != null)
        {
            Destroy(this.films[this.currentFilmNum]);
        }

        this.films[this.currentFilmNum] = filmingObj;

        this.films[this.currentFilmNum].SetActive(false);
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

    //FPSモード/TPSモード変更処理
    private void ChangeFTMode()
    {
        FPSCamera.SetActive(!FPSCamera.activeSelf);
        TPSCamera.SetActive(!TPSCamera.activeSelf);
        isFPSMode = !isFPSMode;
    }

    private void AddPhantom(GameObject film)
    {
        if (this.phantoms[0] != null)
        {
            GameObject next = film; //次に代入するやつ
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
            this.phantoms[0] = film;
        }
    }
}
