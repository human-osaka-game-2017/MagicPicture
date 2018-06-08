using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCtrl : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 槍の発射移動
    virtual public void Action(float _speed)
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    // リセット
    virtual public void Reset()
    {
        Vector3 resetPos = new Vector3(
            0, 0, transform.localPosition.z);

        transform.localPosition -= resetPos;
    }
}