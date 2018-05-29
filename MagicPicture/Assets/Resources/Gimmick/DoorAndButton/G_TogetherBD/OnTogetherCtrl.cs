using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTogetherCtrl : MonoBehaviour {
    
    public int   onButtonCount;
    public bool  closeFlag;
    [Header("↑=== dont touch ===↑")]

    [Space(0), Tooltip("正解のボタン数を入力")]
    public int   correctButtonNum;
    public float openRange;
    public float closeRange;
    public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}