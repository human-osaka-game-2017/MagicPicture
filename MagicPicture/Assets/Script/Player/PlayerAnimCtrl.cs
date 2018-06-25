using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour {

    [SerializeField] float animFwdSpeed;
    [SerializeField] float animBackSpeed;
    [SerializeField] float animHrznSpeed;


    Animator animCtrl;
    float    time2;

    // Use this for initialization
    void Start() {
        animCtrl = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
        if (GameState.state == (int)state.play) {
            
            float waitVertical = Input.GetAxis("VerticalForMove");
            float waitHorizon  = Input.GetAxis("HorizontalForMove") * animHrznSpeed;

            if (waitVertical > 0) {
                animCtrl.speed = animFwdSpeed;
            }
            if (waitVertical < 0) {
                waitVertical *= -1;
                animCtrl.speed = animBackSpeed;
            }
            if (waitHorizon < 0) {
                waitHorizon *= -1;
            }

            animCtrl.SetFloat("Speed", waitVertical + waitHorizon);
        }
        else {
            animCtrl.SetFloat("Speed", 0);  // モーションストップ
        }
    }
}