using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapNeedle : MonoBehaviour {

    public GameObject needlePrefab;

  
    public GameObject needleOn;

    GameObject item;


    private GameObject needle;          //

    // Use this for initialization
    void Start () {
        
        needle = GameObject.Find("Needle");     //
        needleOn = GameObject.Find("NeedleOn");
    }
	
	// Update is called once per frame
	void Update () {

       


            }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "a")  //タグをマジカメで置くオブジェクトに置き換える
        {                             //NieedleOn = false;
                                      //Debug.Log("ニードル停止");
            
        }
        //if (other.gameObject.tag == "a")
        if (other.gameObject.tag == "b")  //タグをマジカメで置くオブジェクトに置き換える
        {
            item = Instantiate(needlePrefab, transform.position, Quaternion.identity);
            StartCoroutine(kesuCoroutine());

           
            Debug.Log("asd");
        }

    }
    IEnumerator kesuCoroutine()
    {
         yield return new WaitForSeconds(0.5f);
        Destroy(item);
    }


    }
