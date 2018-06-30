using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour {

    [SerializeField] SEManager SE;

    [SerializeField] GameObject   doorR;
    [SerializeField] GameObject   doorL;
    [SerializeField] GameObject   cantBack;
    [SerializeField] CloseTrigger closePanel;
    [SerializeField] float        minRange;
    [SerializeField] float        maxRange;
    [SerializeField] float        speed;
    
    private Vector3 speedVec;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        speedVec = Vector3.right * speed * Time.deltaTime;
    }


    //-------
    // 開く
    virtual public void Open()
    {
        float doorPosX = doorR.transform.localPosition.x;

        if (!closePanel.closeFlag) {

            SE.Play("SE_Gimmick_DoorOpen");

            if (doorPosX < maxRange) {
                doorR.transform.Translate(speedVec);
                doorL.transform.Translate(-speedVec);
            }
            else {
                // 開きすぎた分
                Adjust(maxRange);
            }

            // 通れるようにする
            cantBack.SetActive(false);
        }
    }

    //---------
    // 閉じる
    virtual public void Close()
    {
        float doorPosX = doorR.transform.localPosition.x;

        SE.Reuse();

        if (doorPosX > minRange) {
            doorR.transform.Translate(-speedVec);
            doorL.transform.Translate(speedVec);
        }
        else {
            // 閉じすぎた分
            Adjust(minRange);
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