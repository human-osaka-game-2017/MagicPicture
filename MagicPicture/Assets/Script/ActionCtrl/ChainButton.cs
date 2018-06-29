using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainButton : MonoBehaviour {

    [SerializeField] private ChainButtonTrigger trigger;
    [SerializeField] private int number;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision other)
    {
        // 順番通り押すボタンなので、順番通りでないと無反応

        if (trigger.GetState() == number) {
            trigger.SetState(number + 1);   // 次のボタン待機
        }
    }

    void OnCollisionExit(Collision other)
    {
        // (例) 5個のボタンで3を離したら1,2は有効、3,4,5は再度押す

        if (trigger.GetState() >= number) {
            trigger.SetState(number);       // 離されたボタン以上は無効化
        }
    }
}