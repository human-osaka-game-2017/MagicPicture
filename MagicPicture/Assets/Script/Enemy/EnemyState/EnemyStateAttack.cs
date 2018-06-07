using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStateAttack : EnemyStateBase
{
    //敵を見失ってもしばらくはそこにいるだろうで動く
    //完全に見失うまでのカウント
    private const int lostTime = 180;
    private bool isMissing = false;
    private Vector3 targetPos;
    private GameObject target;
    private float dashDistance;
    private float timeElapsed = 0.0f;
    private Vector3 nextPoint;
    private float dashSpeed;
    protected float speed
    {
        get
        {
            if ((targetPos - this.obj.transform.position).magnitude < dashDistance)
            {
                return dashSpeed;
            }
            else
            {
                return defaultSpeed;
            }
        }
    }

    public EnemyStateAttack(GameObject obj, Finder finder, float defaultSpeed, float dashSpeed) : base(obj, finder, defaultSpeed)
    {
        this.defaultSpeed = defaultSpeed;
        this.dashSpeed = dashSpeed;
        this.target = this.finder.FoundList[0].Obj;
        targetPos = target.transform.position;
    }

    override
    protected void Found(GameObject foundObject)
    {
        isMissing = false;
        target = foundObject;
    }

    override
    protected void Lost(GameObject foundObject)
    {
        isMissing = true;
    }

    override
    public EnemyAI.STATE Update()
    {
        EnemyAI.STATE ret = EnemyAI.STATE.ATTACK;

        if (this.isMissing)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= lostTime)
            {
                ret = EnemyAI.STATE.SEARCH;
            }
        }
        else
        {
            timeElapsed = 0.0f;
            targetPos = target.transform.position;
        }

        Vector3 nextPoint = RouteSearch.ObtainNextPos(targetPos, this.obj.transform.position);

        Vector3 movement = nextPoint - this.obj.transform.position;
        movement = movement.normalized * speed * Time.deltaTime;

        this.obj.transform.position += movement;

        return ret;
    }
}