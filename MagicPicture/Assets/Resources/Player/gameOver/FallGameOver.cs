using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGameOver : MonoBehaviour {

    GameObject  m_FallGameOver;
    float       FallHeight;

    // Use this for initialization
    void Start () {
        m_FallGameOver = GameObject.Find("FallGameOver");

        FallHeight = m_FallGameOver.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        GameOversFall();
    }


    void GameOversFall()
    {
        if (transform.position.y < FallHeight) {
            gameObject.SetActive(false);

            Debug.Log("GameOver!");
        }
    }
}
