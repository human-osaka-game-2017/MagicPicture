using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStateSearch : EnemyStateBase
{
    private EnemyAI.STATE nextState = EnemyAI.STATE.SEARCH;
    private Vector3 targetPos;
    private int actionCnt = 0;

    private /*const*/ Vector3 pointSize = new Vector3(5.0f, 0.0f, 5.0f);
    private /*const*/ int actionNum;

   public EnemyStateSearch(GameObject obj, Finder finder, float defaultSpeed, int actionNum) : base(obj, finder, defaultSpeed)
    {
        this.actionNum = actionNum;
        targetPos = new Vector3();
        DecidedNextPoint();
    }

    override
    protected void Found(GameObject foundObject)
    {
        nextState = EnemyAI.STATE.ATTACK;
    }

    override
    protected void Lost(GameObject foundObject)
    {
    }

    override
    public EnemyAI.STATE Update()
    {
        EnemyAI.STATE ret = nextState;

        //次に行く場所を更新
        if(CheckInPoint(this.targetPos, this.obj.transform.position))
        {
            DecidedNextPoint();
            ++this.actionCnt;
        }

        if (this.actionCnt == this.actionNum)
        {
            ret = EnemyAI.STATE.BREAKTIME;
        }

        Vector3 nextPoint = RouteSearch.ObtainNextPos(targetPos, this.obj.transform.position);

        Vector3 movement = nextPoint - this.obj.transform.position;
        movement = movement.normalized * this.defaultSpeed * Time.deltaTime;

        this.obj.transform.position += movement;

        return ret;
    }

    private void DecidedNextPoint()
    {
        targetPos.x = UnityEngine.Random.Range(-15.0f, 15.0f);
        targetPos.y = 0.0f;
        targetPos.z = UnityEngine.Random.Range(-15.0f, 15.0f);
    }

    private bool CheckInPoint(Vector3 targetPos,Vector3 point)
    {
        if (targetPos.x - pointSize.x < point.x &&
            point.x < targetPos.x + pointSize.x)
        {
            if (targetPos.z - pointSize.z < point.z &&
                point.z < targetPos.z + pointSize.z)
                return true;
        }

        return false;
    }
}