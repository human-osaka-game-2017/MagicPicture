using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOrLoad : MonoBehaviour {

    public static bool LoadFlag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //=========================================
    // LoadFlagの値を別のスクリプトで変更する
    //=========================================
    public static void SetLoadFlag(bool flag)
    {
        LoadFlag = flag;
    }


    //===============================
    // 別スクリプトでLoadFlagを呼ぶ
    //===============================
    public static bool GetLoadFlag()
    {
        return LoadFlag;
    }
}