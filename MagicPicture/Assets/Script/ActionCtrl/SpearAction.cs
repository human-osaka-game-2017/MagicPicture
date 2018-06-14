using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAction : MonoBehaviour {

    const int element = 5;

    [SerializeField] FlySpearCtrl[]   Spear = new FlySpearCtrl[element];
    [SerializeField] ParticleSystem[] fire  = new ParticleSystem[element];    
    [SerializeField] float            interval;
    [SerializeField] float            speed;
    
    private float     actionTimer;
    private bool      fireFlag;
    private bool      stopFlag = true;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < element; i++) {
            fire[i].Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        if (GameState.state == (int)state.play) {
            if (!stopFlag) {
                ArrowFlies();
            }
        }
        else {
            Reset(); // play中じゃなかったらリセット(gameOver時)
        }
    }


    //-----------
    // 発射合図
    virtual public void Shot()
    {
        fireFlag = true;
        stopFlag = false;
    }

    //-------------
    // 止める合図
    virtual public void Stop()
    {
        fireFlag = false;
    }


    //-------------
    // 矢を飛ばす
    void ArrowFlies()
    {
        if (actionTimer == 0)
        {
            SoundManager.GetInstance().Play("SE_ShootingArrow", SoundManager.PLAYER_TYPE.NONLOOP, true);
            EffectFire();
        }

        actionTimer += Time.deltaTime;
        
        for (int i = 0; i < element; i++) {
            Spear[i].FlySpear(speed);
        }

        // 発射時間がintervalを超えたら
        if (actionTimer > interval) {
            Reset();
            actionTimer = 0;
        }
    }

    //-----------
    // 炎を出す
    void EffectFire()
    {
        for (int i = 0; i < element; i++) {
            fire[i].Play();
        }
    }

    //-------------
    // 位置を戻す
    void Reset()
    {
        for (int i = 0; i < element; i++) {
            Spear[i].Reset();
        }

        if (!fireFlag) stopFlag = true;
    }
}