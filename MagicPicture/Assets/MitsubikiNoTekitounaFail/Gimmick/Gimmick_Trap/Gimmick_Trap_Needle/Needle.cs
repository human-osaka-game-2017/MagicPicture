using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour {
    
    private GameObject trapNeedle;
    public GameObject NeedleOnPrefab;

    // Use this for initialization
    void Start () {
        trapNeedle = GameObject.Find("Trap_Needle");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trap_Needle")
        {
            this.transform.Translate(new Vector3(0, 10, 0));
           // Destroy(gameObject);
            //StartCoroutine(hukkatuCoroutine());
        }
    }
    IEnumerator hukkatuCoroutine()         //
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(NeedleOnPrefab, transform.position, Quaternion.identity);
    }
}



