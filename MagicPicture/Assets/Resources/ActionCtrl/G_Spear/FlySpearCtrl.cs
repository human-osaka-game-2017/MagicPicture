using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpearCtrl : MonoBehaviour {

    private bool    hitOtherFlag;
    private Vector3 keepVec;
    private Vector3 fixedPos;

    // Use this for initialization
    void Start () {
        fixedPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate()
    {
        
    }

    virtual public void FlySpear(float _speed)
    {
        if (!hitOtherFlag) {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else {
            transform.localPosition = keepVec;
        }
    }

    virtual public void Reset()
    {
        transform.localPosition = fixedPos;
        hitOtherFlag = false;
    }

    void OnTriggerStay() {

        keepVec = transform.localPosition;
        hitOtherFlag = true;
    }
}