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

   public EnemyStateSearch(GameObject obj, Finder finder, float defaultSpeed, float defaultRotSpeed, int actionNum) : 
        base(obj, finder, defaultSpeed, defaultRotSpeed)
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
    public void Collision(GameObject other)
    {
        DecidedNextPoint();
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

        //Vector3 nextPoint = RouteSearch.ObtainNextPos(targetPos, this.obj.transform.position);
        Vector3 nextPoint = targetPos;

        //todo 基底クラスで移動処理をまとめる
        Vector2 pointDir = new Vector2(nextPoint.x, nextPoint.z) - new Vector2(this.obj.transform.position.x, this.obj.transform.position.z);
        Vector2 forward = new Vector2(this.obj.transform.forward.x, this.obj.transform.forward.z);

        float cosTheta = Vector2.Dot(pointDir.normalized, forward.normalized);

        float sinTheta = Cal.Cross2D(pointDir.normalized, forward.normalized);

        float deg = 0;

        if (sinTheta < 0)
        {
            deg = Mathf.LerpAngle(-defaultSpeed * Time.deltaTime, 0.0f, -Mathf.Acos(cosTheta)) * Mathf.Rad2Deg;
        }
        else if (sinTheta > 0)
        {
            deg = Mathf.LerpAngle(0.0f, defaultSpeed * Time.deltaTime, Mathf.Acos(cosTheta)) * Mathf.Rad2Deg;
        }

        this.obj.transform.Rotate(new Vector3(0, deg, 0));
        Vector3 movement = this.obj.transform.forward * defaultSpeed * Time.deltaTime;

        this.obj.transform.position += movement;

        return ret;
    }

    private void DecidedNextPoint()
    {
        targetPos.x = UnityEngine.Random.Range(-6.0f, 6.0f);
        targetPos.y = 0.0f;
        targetPos.z = UnityEngine.Random.Range(-6.0f, 6.0f);
        targetPos += this.obj.transform.position;
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