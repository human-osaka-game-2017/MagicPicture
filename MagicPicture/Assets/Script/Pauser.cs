using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour
{
    Behaviour[] targets;
    GameObject operatingInstructionImg;
    private bool isPausing = false;

    //描画をさせてから止めるため
    //1フレーム後に処理するためのフラグ
    private bool wasPushedButton = false;

    private void Start()
    {
        targets = this.transform.Find("Pause").GetComponentsInChildren<Behaviour>();
        operatingInstructionImg = this.transform.Find("Canvas").gameObject;
    }

    private void Update()
    {
        if (this.wasPushedButton)
        {
            if (this.isPausing)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            this.isPausing = !this.isPausing;
        }

        wasPushedButton = false;

        if (Input.GetButtonDown("ForPause"))
        {
            wasPushedButton = true;
            if (this.isPausing)
            {
                operatingInstructionImg.SetActive(false);

            }
            else
            {
                operatingInstructionImg.SetActive(true);

            }
        }
    }

    private void Pause()
    {
        foreach (Behaviour target in targets)
        {
            if (target != null) target.enabled = false;
        }
        Time.timeScale = 0.0f;
    }

    private void Resume()
    {
        foreach (Behaviour target in targets)
        {
            if (target != null) target.enabled = true;
        }
        Time.timeScale = 1.0f;
    }
}