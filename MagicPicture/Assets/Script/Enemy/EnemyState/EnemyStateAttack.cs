using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStateAttack : EnemyStateBase
{
    //敵を見失ってもしばらくはそこにいるだろうで動く
    //完全に見失うまでのカウント
    /*[SerializeField]*/ private float lostTime = 3;
    private bool isMissing = false;
    private Vector3 targetPos;
    private GameObject target;
    private float dashDistance;
    private float timeElapsed = 0.0f;
    private Vector3 nextPoint;
    private float dashSpeed;
    private float dashRotSpeed;

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

    protected float rotSpeed
    {
        get
        {
            if ((targetPos - this.obj.transform.position).magnitude < dashDistance)
            {
                return dashRotSpeed;
            }
            else
            {
                return defaultRotSpeed;
            }
        }
    }

    public EnemyStateAttack(GameObject obj, Finder finder, float defaultSpeed, float dashSpeed, float defaultRotSpeed, float dashRotSpeed) : 
        base(obj, finder, defaultSpeed, defaultRotSpeed)
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
    public void Collision(GameObject other)
    {
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

        ////todo 基底クラスで移動処理をまとめる
        //Vector2 dir = new Vector2(nextPoint.x, nextPoint.z) - new Vector2(this.obj.transform.position.x, this.obj.transform.position.z);
        ////float PosToNextPoint_deg = Vector2.Angle(Vector2.right, dir);
        //float PosToNextPoint_deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        ////float forward_deg = Vector2.Angle(Vector2.right, new Vector2(this.obj.transform.forward.x, this.obj.transform.forward.z));
        //float forward_deg = Mathf.Atan2(this.obj.transform.forward.y, this.obj.transform.forward.x) * Mathf.Rad2Deg;

        //float deg;
        //if(Math.Abs(PosToNextPoint_deg- forward_deg) > rotSpeed * Time.deltaTime)
        //{
        //    deg = (PosToNextPoint_deg - forward_deg) / Math.Abs(PosToNextPoint_deg - forward_deg) * defaultRotSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    deg = PosToNextPoint_deg - forward_deg;
        //}

        //this.obj.transform.Rotate(new Vector3(0, deg, 0)); 
        //Vector3 movement = this.obj.transform.forward * speed * Time.deltaTime;

        //this.obj.transform.position += movement;

        return ret;
    }
}