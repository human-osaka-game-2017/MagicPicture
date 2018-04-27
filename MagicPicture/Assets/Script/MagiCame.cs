using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour {

    [SerializeField] private /*const*/ int kFilmNum;

    private int currentFilmNum = 0;
    private GameObject[] films;
    private GameObject player= null;
    private Vector3 direction;
    private Ray ray;

    //private void Awake()
    //{

    //}

    void Start ()
    {
        this.player = GameObject.Find("Player");
        this.ray = new Ray(this.transform.position, this.transform.forward.normalized);
        this.films = new GameObject[kFilmNum];
    }

    void Update () {

        this.ray.direction = this.transform.forward.normalized;

        //撮影部
        {
            //レイ表
            Debug.DrawRay(this.ray.origin, this.ray.direction, Color.red);

            if (this.player.GetComponent<ModeChange>().IsFPSMode)
            {
                // Rayが衝突したコライダーの情報を得る
                RaycastHit hit;

                if (Physics.Raycast(this.ray, out hit))
                {
                    //hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                    //シャッターを切られた
                    if (/*!player.m_IsMoving &&*/Input.GetKeyDown(KeyCode.Space))
                    {
                        this.films[this.currentFilmNum] = Instantiate(hit.collider.gameObject, /*new Vector3()*/transform.position, Quaternion.identity);

                        // Rayの原点から衝突地点までの距離を得る
                        float dis = hit.distance;

                        this.films[this.currentFilmNum].SetActive(false);
                    }
                }
            }
        }

        //現像部
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.films[this.currentFilmNum].transform.position = this.transform.position + (this.transform.forward.normalized * 2);
                this.films[this.currentFilmNum].SetActive(true);
            }
        }
    }
}