using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AxisStateManager
{
    static AxisStateManager instance = null;

    public static AxisStateManager GetInstance()
    {
        return instance ?? (instance = new AxisStateManager());
    }

    //押された瞬間に1or-1を返す
    //それ以外は0
    public int GetAxisDown(string key)
    {
        if (!this.axis.ContainsKey(key))
        {
            this.axis[key] = AXIS_STATE.OFF;
        }

        if (this.axis[key] == AXIS_STATE.DOWN)
        {
            if (Input.GetAxisRaw(key) < 0) return -1;
            else return 1;
        }
        return 0;
    }

    private GameObject obj = new GameObject("AxisUpdater");
    AxisStateManager()
    {
        if(this.obj == null)
        {
            obj = new GameObject("AxisUpdater");
        }

        obj.AddComponent<AxisUpdater>();
        GameObject.DontDestroyOnLoad(this.obj);
    }

    private enum AXIS_STATE
    {
        OFF,
        DOWN,
        RELEASE,
        UP
    }

    private Dictionary<string, AXIS_STATE> axis = new Dictionary<string, AXIS_STATE>();

    private float deadZone = 0.02f;

    //1フレに1度だけ読んでいただく
    //c#にフレンドがないのでiternalなるものを調べたがようわからん
    //また調べること
    public void Update()
    {
        List<string> keyList = new List<string>(this.axis.Keys);

        foreach (string key in keyList)
        {
            switch (this.axis[key])
            {
                case AXIS_STATE.OFF:
                    if (Input.GetAxis(key) < -deadZone || deadZone  < Input.GetAxis(key))
                    {
                        this.axis[key] = AXIS_STATE.DOWN;
                    }
                    break;

                case AXIS_STATE.DOWN:
                    this.axis[key] = AXIS_STATE.RELEASE;
                    break;

                case AXIS_STATE.RELEASE:
                    if ( -deadZone < Input.GetAxis(key) && Input.GetAxis(key) < deadZone)
                    {
                        this.axis[key] = AXIS_STATE.UP;
                    }
                    break;

                case AXIS_STATE.UP:
                    this.axis[key] = AXIS_STATE.OFF;
                    break;
            }
        }
    }
}