using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem: MonoBehaviour {

    [SerializeField] private GameObject FPSCamera;
    [SerializeField] private GameObject TPSCamera;
    [SerializeField] private /*const*/ int kMaxFilm;

    private GameObject[] films;
    private int currentFilmNum = 0;
    public int GetCurrentFilmNum()
    {
        return currentFilmNum;
    }

    private bool isFPSMode = false;
    public bool IsFPSMode
    {
        get { return this.isFPSMode; }
        private set { this.isFPSMode = value; }
    }

    void Start ()
    {
        this.films = new GameObject[kMaxFilm];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ChangeFTMode();

        //フィルム選択の更新
        UpdateCurrentFilmNum();

        //現像
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.films[this.currentFilmNum].transform.position = this.transform.position + (this.transform.forward.normalized * 2);
            this.films[this.currentFilmNum].SetActive(true);
        }

    }

    public void SetFilm(GameObject filmingObj)
    {
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
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (this.currentFilmNum == this.kMaxFilm)
            {
                this.currentFilmNum = this.kMaxFilm;
            }
            else
            {
                ++this.currentFilmNum;
            }
        }
    }

    //FPSモード/TPSモード変更処理
    private void ChangeFTMode()
    {
        FPSCamera.SetActive(!FPSCamera.activeSelf);
        TPSCamera.SetActive(!TPSCamera.activeSelf);
        isFPSMode = !isFPSMode;
    }
}
