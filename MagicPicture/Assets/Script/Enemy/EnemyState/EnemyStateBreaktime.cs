using UnityEngine;

class EnemyStateBreaktime : EnemyStateBase
{
    private float timeElapsed = 0.0f;
    private float breakInterval;

    EnemyAI.STATE nextState = EnemyAI.STATE.BREAKTIME;

    public EnemyStateBreaktime(GameObject obj, Finder finder, float breakInterval) : 
        base(obj, finder)
    {
        this.breakInterval = breakInterval;
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
    }

    override
    public EnemyAI.STATE Update()
    {
        EnemyAI.STATE ret = nextState;

        this.timeElapsed += Time.deltaTime;

        if(timeElapsed >= breakInterval)
        {
            ret = EnemyAI.STATE.SEARCH;
        }

        return ret;
    }
}