using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour {

    [SerializeField] GameObject   doorR;
    [SerializeField] GameObject   doorL;
    [SerializeField] GameObject   cantBack;
    [SerializeField] CloseTrigger closePanel;
    [SerializeField] float        speed;

    public  bool    resetFlag;  // trueなら感圧板から離れた後でもOpenが有効(falseなら無効)
    private float   doorPosX;
    private Vector3 speedVec;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        doorPosX = doorR.transform.localPosition.x;
        speedVec = Vector3.right * speed * Time.deltaTime;
    }


    //-------
    // 開く
    virtual public void Open()
    {
        if (!closePanel.closeFlag)

        if (doorPosX < 2) {
            doorR.transform.Translate(speedVec);
            doorL.transform.Translate(-speedVec);
        }
        else {
            // 開きすぎた分
            Adjust(2);
        }
        
        // 通れるようにする
        cantBack.SetActive(false);
    }

    //---------
    // 閉じる
    virtual public void Close()
    {
        if (doorPosX > 0.75f) {
            doorR.transform.Translate(-speedVec);
            doorL.transform.Translate(speedVec);
        }
        else {
            // 閉じすぎた分
            Adjust(0.75f);
        }

        // 通れないようにする(挟まり防止)
        cantBack.SetActive(true);
    }


    //----------------------------------
    // Rigitを付けない代わりに調整する
    void Adjust(float _range)
    {
        float posX = doorR.transform.localPosition.x - _range;

        doorR.transform.Translate(Vector3.left  * posX);
        doorL.transform.Translate(Vector3.right * posX);
    }
}