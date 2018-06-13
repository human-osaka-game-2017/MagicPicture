using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour {

    [SerializeField] float animFwdSpeed;
    [SerializeField] float animBackSpeed;

    Animator animCtrl;
    float    time2;

    // Use this for initialization
    void Start() {
        animCtrl = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
        if (GameState.state == (int)state.play) {

            float time;

            time2 = time = Input.GetAxis("VerticalForMove");

            if (time > 0) animCtrl.speed = animFwdSpeed;
            if (time < 0) animCtrl.speed = animBackSpeed;

            if (time2 < 0) time2 *= -1;

            animCtrl.SetFloat("Speed", time2);
        }
        else {
            animCtrl.SetFloat("Speed", 0);  // モーションストップ
        }
    }
}