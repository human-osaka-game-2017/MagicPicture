using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSpear : MonoBehaviour {
    
    [SerializeField] OnGimmickSpear onGimmickSpear;
    [SerializeField] SpearCtrl[]    Spear = new SpearCtrl[5];
    [SerializeField] SpeaFireCtrl[] Fire  = new SpeaFireCtrl[5];

    public  float speed;
    public  float interval;
    private float actionTimer;
    
    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update()
    {
        // 感圧板に触れたかactionTimerが動作中なら
        if (onGimmickSpear.GetOnFlag() || actionTimer > 0) {
            actionTimer += Time.deltaTime;
        }

        // actionTimerがintervalに達したら
        if (actionTimer > interval) {

            for (int i = 0; i < 5; i++) {
                actionTimer = 0;    // actionTimerのリセット
                Spear[i].Reset();   // 槍の位置をリセット
            }
        }
    }


    void FixedUpdate()
    {
        // actionTimerが動作中なら
        if (actionTimer > 0) {
            
            for (int i = 0; i < 5; i++) {

                Spear[i].Action(speed); // 槍の発射移動
                Fire[i].Action();       // 炎エフェクト発生
            }
        }
    }
}