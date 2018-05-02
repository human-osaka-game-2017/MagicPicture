using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameOver : MonoBehaviour {

    float rotationX;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationX , 0, 0);

        if (rotationX != 0) {
            if (transform.rotation.x > 0) {
                gameObject.SetActive(false);

                Debug.Log("GameOver!");
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyTag"){
            
            if (transform.rotation.x < 0) {
                rotationX = 2.5f;
            }
        }
    }
}