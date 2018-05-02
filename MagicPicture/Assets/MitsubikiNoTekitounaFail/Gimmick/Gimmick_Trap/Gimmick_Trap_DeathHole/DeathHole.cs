using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "a")  //タグをプレイヤーに置き換える
         Debug.Log("死んだ");　　//ここにゲームオーバー処理を追記する
    }
}
