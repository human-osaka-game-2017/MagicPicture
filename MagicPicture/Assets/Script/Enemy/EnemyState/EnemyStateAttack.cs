using UnityEngine;

class EnemyStateAttack : EnemyStateBase
{
    //敵を見失ってもしばらくはそこにいるだろうで動く
    //完全に見失うまでのカウント
    private float lostTime = 3;
    private bool isMissing = false;
    private Vector3 targetPos;
    private GameObject target;
    private float dashDistance;
    private float timeElapsed;
    private float dashSpeed;
    private float dashRotSpeed;
    private float defaultSpeed;
    private float defaultRotSpeed;

    public EnemyStateAttack(GameObject obj, Finder finder, float dashSpeed, float dashRotSpeed) : 
        base(obj, finder)
    {
        this.dashRotSpeed = dashRotSpeed;
        this.dashSpeed = dashSpeed;
        this.defaultSpeed = this.NavAgent.speed;
        this.defaultRotSpeed = this.NavAgent.angularSpeed;
        this.target = this.Finder.FoundList[0].Obj;
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

        this.NavAgent.SetDestination(targetPos);
        //if(navMeshAgent.remainingDistance < this.dashDistance)
        //{
        //    this.navMeshAgent.speed = this.dashSpeed;
        //    this.navMeshAgent.angularSpeed = this.dashRotSpeed;
        //}
        //else
        //{
        //    this.navMeshAgent.speed = this.defaultSpeed;
        //    this.navMeshAgent.angularSpeed = this.defaultRotSpeed;
        //}

        return ret;
    }
}