using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSpear : MonoBehaviour {

    [SerializeField] GetSpear       Spear;
    [SerializeField] OnGimmickSpear onGimmickSpear;

    public  float       speed = 20;                 // デフォルト値
    public  float       shootingInterval = 0.5f;    // デフォルト値
    private bool        flag;
    private bool        flag2;
    private float       firingTimer;
    private Vector3[]   resetPos = new Vector3[5];
    private GetSpear[]  spear    = new GetSpear[5];


    // Use this for initialization
    void Start()
    {
        resetPos[1].x = resetPos[2].x =  0.57f;     // 左
        resetPos[1].y = resetPos[3].y =  0.62f;     // 上
        resetPos[2].y = resetPos[4].y = -0.58f;     // 下
        resetPos[3].x = resetPos[4].x = -0.55f;     // 右
        
        // 槍のセッティング
        for (int i = 0; i < 5; i++) {
            spear[i] = Instantiate(Spear);  // 槍を複製
            RisetSpear(i);                  // 槍情報リセット(セット)
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 感圧板に触れたかfiringTimerが動作中なら
        if (onGimmickSpear.GetOnFlag() || firingTimer > 0) {
            firingTimer += Time.deltaTime;
        }

        // firingTimerが動作中なら
        if (firingTimer > shootingInterval) {

            for (int i = 0; i < 5; i++) {
                firingTimer = 0;    // firingTimerのリセット
                RisetSpear(i);      // 槍情報リセット
            }
        }
    }


    void FixedUpdate()
    {
        // firingTimerが動作中なら
        if (firingTimer > 0) {

            for (int i = 0; i < 5; i++) {

                // 槍の移動
                spear[i].transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }


    //=================
    // 槍情報リセット
    //=================
    private void RisetSpear(int _i)
    {
        spear[_i].transform.rotation = transform.rotation;                  // 槍の方向をGimmickSpear(Empty)に合わせる
        spear[_i].transform.position = transform.position + resetPos[_i];   // 槍の位置を再セット
    }
}