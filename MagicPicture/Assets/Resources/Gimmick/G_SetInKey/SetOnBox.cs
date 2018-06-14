using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnBox : MonoBehaviour {

    [SerializeField] GameObject      nonActiveObj;
    [SerializeField] ParticleSystem  pole;
    [SerializeField] ParticleSystem  bottom;

    public  string    designationObj;
    public  Vector3   rangeScale;
    public  Vector3   rangePoint_Rot;
    public  Vector3   minRange;
    public  Vector3   maxRange;
    private Vector3   surplus;
    bool flag;

    // Use this for initialization
    void Start () {
        pole.Stop();
        surplus.x = surplus.y = surplus.z = 0.25f;
    }
	
	// Update is called once per frame
	void Update () {

        // Rayと接触したオブジェクトが入る
        RaycastHit hitCol;

        // Rayのカスタマイズ
        Ray ray = new Ray(transform.position, transform.up * 10);        

        // Rayの表示
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 3.0f);
        
        // Rayとオブジェクトが接触していたら
        if (Physics.Raycast(ray, out hitCol)) {
            
            Vector3 rot  = hitCol.transform.rotation.eulerAngles;
            Vector3 minR = rangePoint_Rot - minRange; // 最小範囲
            Vector3 maxR = rangePoint_Rot + maxRange; // 最大範囲
            

            // 回転値が許容範囲内なら
            if (InRangeRot(rot, maxR, minR)) {
                
                Vector3 scale = hitCol.transform.localScale;
                Vector3 minS  = rangeScale - surplus; // 最小範囲
                Vector3 maxS  = rangeScale + surplus; // 最大範囲


                // スケール値が許容範囲内なら
                if (InRangeScale(scale, maxS, minS)) {
                    
                    // 特にオブジェの指定なし
                    if (designationObj == "") {

                        nonActiveObj.SetActive(false);

                        if (!flag) {
                            pole.Play();
                            bottom.Stop();
                            flag = true;
                        }
                    }
                    // オブジェの指定あり
                    if (designationObj == hitCol.collider.gameObject.name) {

                        nonActiveObj.SetActive(false);

                        if (!flag) {
                            pole.Play();
                            bottom.Stop();
                            flag = true;
                        }
                    }
                }
            }
        }
	}


    //===========================
    // 回転値が許容範囲内か判定
    //===========================
    bool InRangeRot(Vector3 _rot, Vector3 _max, Vector3 _min)
    {
        int inCount = 0;
        
        if (RotInRange(_rot.x, _max.x, _min.x)) inCount++;  // Rx
        if (RotInRange(_rot.y, _max.y, _min.y)) inCount++;  // Ry
        if (RotInRange(_rot.z, _max.z, _min.z)) inCount++;  // Rz

        if (inCount == 3) {
            return true;
        }

        return false;
    }


    //===============================
    // スケール値が許容範囲内か判定
    //===============================
    bool InRangeScale(Vector3 _scale, Vector3 _max, Vector3 _min)
    {
        int inCouont = 0;

        if (InRange(_scale.x, _max.x, _min.x)) inCouont++;  // Sx
        if (InRange(_scale.y, _max.y, _min.y)) inCouont++;  // Sy
        if (InRange(_scale.z, _max.z, _min.z)) inCouont++;  // Sz

        if (inCouont == 3) {
            return true;
        }
        
        return false;
    }


    //=============
    // 範囲内か？
    //=============
    bool InRange(float _target, float _max, float _min)
    {
        // 回転値が範囲内ならture
        if (_target < _max &&
            _target > _min) {
            return true;
        }

        return false;
    }


    //===================
    // 回転は範囲内か？
    //===================
    bool RotInRange(float _target, float _max, float _min)
    {
        // Rotationが180度以降マイナスになることを考慮

        if (_max + _min < 180) {
            // 180度以降マイナスに
            if (_target > 180) _target -= 360;

            return InRange(_target, _max, _min);
        }
        if (_max + _min > 180) {
            return InRange(_target, _max, _min);
        }

        return false;
    }
}