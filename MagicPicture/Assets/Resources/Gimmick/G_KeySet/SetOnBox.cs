using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnBox : MonoBehaviour {

    [SerializeField] TmpProcessing tmp;

    public Vector3    maxRangeSize;
    public Vector3    minRangeSize;
    public Quaternion maxRangeRot;
    public Quaternion minRangeRot;

    public int keyObjNum;

    private GameObject[] keyObj = new GameObject[2];

    // Use this for initialization
    void Start () {

		for (int i = 0; i < 2; i++) {
            keyObj[i] = GameObject.Find("KeyObj (" + i + ")");
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "KeyObjTag") {

            for (int i = 0; i < 2; i++) {
                if (col.gameObject.name == keyObj[i].name) {

                    if (keyObj[i].transform.rotation.y < maxRangeRot.y &&
                        keyObj[i].transform.rotation.y > minRangeRot.y) {

                        tmp.okFlag = true;
                    }
                }
            }
        }
    }
}