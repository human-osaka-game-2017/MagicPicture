using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStateFactory
{
    private GameObject obj;
    private Finder finder;
    private float defaultSpeed;
    private float dashSpeed;
    private float breakInterval;
    private int actionNum;

    public EnemyStateFactory(GameObject obj, Finder finder, float defaultSpeed, float dashSpeed, float breakInterval, int actionNum)
    {
        this.obj = obj;
        this.finder = finder;
        this.defaultSpeed = defaultSpeed;
        this.dashSpeed = dashSpeed;
        this.actionNum = actionNum;
        this.breakInterval = breakInterval;
    }

    public EnemyStateBase Create(EnemyAI.STATE state)
    {
        EnemyStateBase ret = null;

        switch (state)
        {
            case EnemyAI.STATE.ATTACK:
                ret = new EnemyStateAttack(this.obj, this.finder, this.defaultSpeed, this.dashSpeed);
                break;

            case EnemyAI.STATE.BREAKTIME:
                ret = new EnemyStateBreaktime(this.obj, this.finder, this.defaultSpeed, this.breakInterval);
                break;

            case EnemyAI.STATE.SEARCH:
                ret = new EnemyStateSearch(this.obj, this.finder, this.defaultSpeed, this.actionNum);
                break;
        }

        return ret;
    }
}