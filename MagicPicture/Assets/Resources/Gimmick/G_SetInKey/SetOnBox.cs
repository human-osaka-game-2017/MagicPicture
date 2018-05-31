using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnBox : MonoBehaviour {

    [SerializeField] TmpProcessing tmp;

    public Vector3Int absoluteScale;
    public Vector3    rangePoint_Rot;
    public Vector3    minRange;
    public Vector3    maxRange;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        // Rayと接触したオブジェクトが入る
        RaycastHit hitCol;

        // Rayのカスタマイズ
        Ray ray = new Ray(transform.position, transform.forward * 10);        

        // Rayの表示
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 3.0f);

        // Rayとオブジェクトが接触していたら
        if (Physics.Raycast(ray, out hitCol)) {

            bool inRangeFlag = false;
            
            Vector3 min    = rangePoint_Rot - minRange; // 最小範囲
            Vector3 max    = rangePoint_Rot + maxRange; // 最大範囲
            Vector3 target = hitCol.transform.rotation.eulerAngles; // Rayに接触したObj


            // 回転値は許容範囲内か？ Rx, Ry, Rz
            if (!inRangeFlag) inRangeFlag = RotInRange(target.x, max.x, min.x);   // Rx
            if (inRangeFlag)  inRangeFlag = RotInRange(target.y, max.y, min.y);   // Ry
            if (inRangeFlag)  inRangeFlag = RotInRange(target.z, max.z, min.z);   // Rz
            if (inRangeFlag) {                                                    // scale

                Vector3    targetScale = hitCol.transform.localScale;
                Vector3Int scaleInt = Vector3Int.zero;

                // float値の整数化(一応)
                scaleInt.x = (int)targetScale.x;
                scaleInt.y = (int)targetScale.y;
                scaleInt.z = (int)targetScale.z;


                // スケールの許容範囲(スケールは3段階)
                if (scaleInt == absoluteScale) {

                    // したい処理を書く
                    tmp.okFlag = true;
                }
            }
        }
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