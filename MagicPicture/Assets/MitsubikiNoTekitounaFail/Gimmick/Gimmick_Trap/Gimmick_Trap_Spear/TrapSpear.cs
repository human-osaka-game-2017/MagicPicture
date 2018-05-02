using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpear : MonoBehaviour {

    public GameObject SpearPrefab;
    public GameObject spear;

    private GameObject spearTrigger;


    public float dropSpeed = 5f;

    // Use this for initialization
    void Start () {
        spearTrigger = GameObject.Find("SpearTrigger");
        //x = 0;
        //y = 0;
        //z = 0;
        GoSpear();
    }
	
	// Update is called once per frame
	void Update () {
       
           

            //spear = (GameObject)Instantiate(SpearPrefab);
            
            

            //spear.transform.Translate(0, this.dropSpeed, 0);
            //if (spear.transform.position.y < 3f)         //
            //{
                //Destroy(this);
            //}

            Debug.Log("スピアー発射");
        

        

    }
    void GoSpear()
    {
        if (spearTrigger.GetComponent<SpearTrigger>().SpearOn == true)
        {
            Instantiate(SpearPrefab, transform.position, Quaternion.identity);
        }
    }


}