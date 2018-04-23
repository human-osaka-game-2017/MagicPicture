using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiCame : MonoBehaviour {

    public LayerMask mask;

    [SerializeField] private GameObject fourcs;

    private GameObject m_GameManager = null;
    private GameObject m_Player;
    private Vector3 m_Direction;
    private Ray m_Ray;

    private void Awake()
    {
        m_GameManager   = GameObject.Find("GM");
        m_Player        = GameObject.Find("Player");
        m_Ray           = new Ray(transform.position, transform.forward.normalized);
    }

    void Start () {
       
    }

    void Update () {

        //レイ表示
        Debug.DrawRay(m_Ray.origin, m_Ray.direction, Color.red);

        if (m_GameManager.GetComponent<GameManager>().IsFPSMode)
        {
            // Rayが衝突したコライダーの情報を得る
            RaycastHit hit;

            // 衝突したオブジェクトの色を赤に変える
            bool rayIsCollided = Physics.Raycast(m_Ray, out hit, 10000, mask);

            if(rayIsCollided)
            {
                hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            }

            if (/*!player.m_IsMoving &&*/Input.GetKeyDown(KeyCode.Space))
            {
                // Rayが衝突したかどうか
                if (rayIsCollided)
                {
                    // Rayの原点から衝突地点までの距離を得る
                    float dis = hit.distance;
                }
            }
        }
    }
}