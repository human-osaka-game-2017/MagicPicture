using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bool3 {
    public bool x;
    public bool y;
    public bool z;
}

public class SetTrigger : MonoBehaviour {

    [SerializeField] ActionCtrl      actionCtrl;
    [SerializeField] ParticleSystem  poleEffect;
    [SerializeField] ParticleSystem  bottomEffect;
    
    [SerializeField] private string  objectName;
    [SerializeField] private Vector3 rangeScl;
    [SerializeField] private Vector3 pointRot;
    [SerializeField] private Vector3 rangeRot;    
    [SerializeField] private Bool3   check_Rot;
    [SerializeField] private Bool3   check_Scl;

    private int  checkCount_Scl;
    private int  checkCount_Rot;
    private bool actionedFlag;
    private bool restedFlag;
    

    // Use this for initialization
    void Start () {
        poleEffect.Stop();
        
        int countRot = 0;

        if (check_Rot.x) countRot++;
        if (check_Rot.y) countRot++;
        if (check_Rot.z) countRot++;

        checkCount_Rot = countRot;

        int countScl = 0;

        if (check_Scl.x) countScl++;
        if (check_Scl.y) countScl++;
        if (check_Scl.z) countScl++;

        checkCount_Scl = countScl;
    }
	
	// Update is called once per frame
	void Update () {

        // Rayと接触したオブジェクトが入る
        RaycastHit hitObj;

        // Rayのカスタマイズ
        Ray ray = new Ray(transform.position, transform.up * 10);

        // Rayの表示
        Debug.DrawRay(ray.origin, transform.up * 10, Color.yellow);

        // Rayとオブジェクトが接触していたら
        if (Physics.Raycast(ray, out hitObj)) {
            
            Vector3 maxR      = pointRot + rangeRot;
            Vector3 minR      = pointRot - rangeRot;
            Vector3 hitObjRot = hitObj.transform.eulerAngles;
            
            if (Range_Rot(hitObjRot, maxR, minR)) {                
                Vector3 hitObjScl = hitObj.transform.localScale;

                if (Range_Scl(hitObjScl)) {
                    string hitName = hitObj.transform.name;

                    // オブジェクトを指定するとそれ以外は通さない
                    if (objectName == "" || objectName == hitName) {
                        actionCtrl.Action();

                        if (!actionedFlag) EffectAction();
                    }
                }
                else {
                    actionCtrl.Reset();

                    if (!restedFlag) EffectReset();
                }
            }
            else {
                actionCtrl.Reset();

                if (!restedFlag) EffectReset();
            }
        }
    }


    //-------------------------
    // このギミックでのAction
    void EffectAction()
    {
        poleEffect.Play();
        bottomEffect.Stop();

        actionedFlag = true;
        restedFlag = false;
    }

    //---------------------------
    // EffectやActionのリセット
    void EffectReset()
    {
        poleEffect.Stop();
        bottomEffect.Play();

        actionedFlag = false;
        restedFlag = true;
    }


    //-----------------------------------
    // 回転値の範囲判定をする(Rx,Ry,Rz)
    bool Range_Rot(Vector3 _target, Vector3 _max, Vector3 _min)
    {
        int inCount = 0;

        if (check_Rot.x && RotInRange(_target.x, _max.x, _min.x)) inCount++;  // Rx
        if (check_Rot.y && RotInRange(_target.y, _max.y, _min.y)) inCount++;  // Ry
        if (check_Rot.z && RotInRange(_target.z, _max.z, _min.z)) inCount++;  // Rz

        if (inCount == checkCount_Rot) {
            return true;
        }

        return false;
    }

    //-------------------------------------
    // スケールの範囲判定をする(Sx,Sy,Sz)
    bool Range_Scl(Vector3 _target)
    {
        int inCouont = 0;

        float range = 0.1f;
        
        if (check_Scl.x && InRange(_target.x, rangeScl.x + range, rangeScl.x - range)) inCouont++;  // Sx
        if (check_Scl.y && InRange(_target.y, rangeScl.y + range, rangeScl.y - range)) inCouont++;  // Sy
        if (check_Scl.z && InRange(_target.z, rangeScl.z + range, rangeScl.z - range)) inCouont++;  // Sz

        if (inCouont == checkCount_Scl) {
            return true;
        }

        return false;
    }


    //-----------------
    // 範囲内ならture
    bool InRange(float _target, float _max, float _min)
    {
        if (_target < _max &&
            _target > _min) {
            return true;
        }

        return false;
    }

    //---------------------------
    // 回転は複雑なので少し考慮
    bool RotInRange(float _target, float _max, float _min)
    {
        // 180度から++が不思議な値になる
        // Rotationを均一になるように考慮(内部的な問題？数学？)

        if (_max + _min < 180) {
            if (_target > 180) _target -= 360;

            return InRange(_target, _max, _min);
        }
        if (_max + _min > 180) {
            return InRange(_target, _max, _min);
        }

        return false;
    }
}