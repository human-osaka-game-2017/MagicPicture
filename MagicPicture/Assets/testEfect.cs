using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEfect : MonoBehaviour
{
    public GameObject E_damagePrefab;
    public GameObject E_appearPrefab;
    public GameObject E_disappearPrefab;
    public GameObject E_fitPrefab;
    public GameObject E_arrowPrefab;
    public GameObject E_IntermediatePointPrefab;
    public GameObject E_DoorOpenPrefab;

    public AudioClip SE_damaage;
    public AudioClip SE_appear;
    public AudioClip SE_disappear;
    public AudioClip SE_fit;
    public AudioClip SE_arrow;
    public AudioClip SE_IntermediatePoint;
    public AudioClip SE_DoorOpen;


    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void E_damaage()
    {
        Instantiate(E_damagePrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_damaage);
    }

    public void E_appear()
    {
        Instantiate(E_appearPrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_appear);
    }

    public void E_disappear()
    {
        Instantiate(E_disappearPrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_disappear);
    }

    public void E_fit()
    {
        Instantiate(E_fitPrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_fit);
    }

    public void E_arrow()
    {
        Instantiate(E_arrowPrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_arrow);
    }

    public void E_IntermediatePoint()
    {
        Instantiate(E_IntermediatePointPrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(SE_IntermediatePoint);
    }

    public void E_DoorOpen()
    {
        GetComponent<AudioSource>().PlayOneShot(SE_DoorOpen);
    }
}
