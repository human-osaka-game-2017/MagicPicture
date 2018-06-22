using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpearCtrl : MonoBehaviour {

    private bool    hitOtherFlag;
    private Vector3 keepVec;
    private Vector3 defaultPos;

    // Use this for initialization
    void Start () {
        defaultPos = transform.localPosition;
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
        transform.localPosition = defaultPos;
        hitOtherFlag = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        // 現像前のobjectに当たってもスルー(layerの2)
        if (col.gameObject.layer != 2) {
            SoundManager.GetInstance().Play("SE_ArrowHit", SoundManager.PLAYER_TYPE.NONLOOP, true);

            keepVec = transform.localPosition;
            hitOtherFlag = true;
        }
    }
}