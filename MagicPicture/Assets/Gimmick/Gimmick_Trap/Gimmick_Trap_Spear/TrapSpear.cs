using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpear : MonoBehaviour {

    public GameObject SpearPrefab;
    public GameObject spear;

    private GameObject spearTrigger;

    public int x;
    public int y;
    public int z;



    public float dropSpeed = 5f;

    // Use this for initialization
    void Start () {
        spearTrigger = GameObject.Find("SpearTrigger");
        //x = 0;
        //y = 0;
        //z = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (spearTrigger.GetComponent<SpearTrigger>().SpearOn == true)
        {
            //spear = (GameObject)Instantiate(SpearPrefab);
            Instantiate(SpearPrefab, transform.position, Quaternion.identity);
            spear.GetComponent<Spear>().Shot(new Vector3(0, 0, 0));

            spear.transform.Translate(0, this.dropSpeed, 0);
            if (spear.transform.position.y < 3f)         //
            {
                //Destroy(this);
            }

            Debug.Log("スピアー発射");
        }
    }
    
   
    
}
