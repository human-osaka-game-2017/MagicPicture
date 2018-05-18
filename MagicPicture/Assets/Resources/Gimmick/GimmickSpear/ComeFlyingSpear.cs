using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeFlyingSpear : MonoBehaviour {
    
    public ComeFlyingSpear Spear;
    private GameObject spearBox;
    private ComeFlyingSpear[] spear = new ComeFlyingSpear[8];
    private bool flag;
    private bool flag2;
    private float time;

    // Use this for initialization
    void Start() {
        spearBox = GameObject.Find("GimmickSpearBox");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("g")) {
            flag = true;
        }

        if (flag) {
            time += Time.deltaTime;

            if (time > 0.5f) {

                for (int i = 0; i < 8; i++) {
                    if (!flag2) {
                        spear[i] = Instantiate(Spear);
                        if (i == 7) flag2 = true;
                    }
                    
                    spear[i].transform.position = spearBox.transform.position +
                        new Vector3(Random.value, Random.value, Random.value);
                }

                time = 0;
            }
        }
	}

    void FixedUpdate()
    {
        if (spear[0] != null) {
            for (int i = 0; i < 8; i++) {
                spear[i].transform.Translate(Vector3.forward * 30 * Time.deltaTime);
            }
        }
    }
}