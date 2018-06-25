using UnityEngine;
using UnityEngine.AI;

class EnemyStateSearch : EnemyStateBase
{
    private EnemyAI.STATE nextState = EnemyAI.STATE.SEARCH;
    private Vector3 targetPos;
    private int actionCnt = 0;

    private Vector3 pointSize = new Vector3(5.0f, 0.0f, 5.0f);
    private int actionNum;

   public EnemyStateSearch(GameObject obj, Finder finder, int actionNum) : 
        base(obj, finder)
    {
        this.NavAgent = this.Obj.GetComponent<NavMeshAgent>();
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
        if(CheckInPoint(this.targetPos, this.Obj.transform.position))
        {
            DecidedNextPoint();
            ++this.actionCnt;
        }

        if (this.actionCnt == this.actionNum)
        {
            ret = EnemyAI.STATE.BREAKTIME;
        }

        this.NavAgent.SetDestination(targetPos);

        return ret;
    }

    private void DecidedNextPoint()
    {
        targetPos.x = UnityEngine.Random.Range(-6.0f, 6.0f);
        targetPos.y = 0.0f;
        targetPos.z = UnityEngine.Random.Range(-6.0f, 6.0f);
        targetPos += this.Obj.transform.position;
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