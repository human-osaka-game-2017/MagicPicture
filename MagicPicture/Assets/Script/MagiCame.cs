using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour {

    [SerializeField] private GameObject fourcs;
    [SerializeField] private int FilmNum;

    private int CurrentFilmNum = 0;
    private GameObject[] Films;
    private GameObject GameManager = null;
    //private GameObject Player;
    private Vector3 Direction;
    private Ray Ray;

    //private void Awake()
    //{

    //}

    void Start ()
    {
        this.GameManager = GameObject.Find("GM");
      //this.Player = GameObject.Find("Player");
        this.Ray = new Ray(transform.position, transform.forward.normalized);
        this.Films = new GameObject[FilmNum];
    }

    void Update () {

        //撮影部
        {
            //レイ表示
            Debug.DrawRay(this.Ray.origin, this.Ray.direction, Color.red);

            if (this.GameManager.GetComponent<GameManager>().IsFPSMode)
            {
                // Rayが衝突したコライダーの情報を得る
                RaycastHit hit;

                if (Physics.Raycast(this.Ray, out hit))
                {
                    hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                    //シャッターを切られた
                    if (/*!player.m_IsMoving &&*/Input.GetKeyDown(KeyCode.Space))
                    {
                        this.Films[this.CurrentFilmNum] = Instantiate(hit.collider.gameObject, /*new Vector3()*/transform.position, Quaternion.identity);

                        // Rayの原点から衝突地点までの距離を得る
                        float dis = hit.distance;

                        this.Films[this.CurrentFilmNum].SetActive(false);
                    }
                }
            }
        }

        //現像部
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.Films[this.CurrentFilmNum].transform.position = this.transform.position;
                this.Films[this.CurrentFilmNum].SetActive(true);
            }
        }
    }
}