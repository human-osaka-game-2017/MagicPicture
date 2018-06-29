using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    List<Behaviour> targets = new List<Behaviour>();
    private GameObject img;
    private bool isPausing = false;

    private void Start()
    {
        img = this.transform.Find("Canvas").gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ForPause"))
        {
            if (this.isPausing)
            {
                Resume();
                img.SetActive(false);

            }
            else
            {
                Pause();
                img.SetActive(true);

            }
            this.isPausing = !this.isPausing;
        }

        if (this.isPausing)
        {
            if (Input.GetButtonDown("ForExitGameInPause"))
            {
                Application.Quit();
            }
        }
    }

    private void Pause()
    {
        targets.AddRange(this.transform.Find("Pause").GetComponentsInChildren<Behaviour>());

        foreach (Behaviour target in targets)
        {
            if (target != null)
            {
                if (!(target is UnityEngine.Camera))
                {
                    target.enabled = false;
                }
            }
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