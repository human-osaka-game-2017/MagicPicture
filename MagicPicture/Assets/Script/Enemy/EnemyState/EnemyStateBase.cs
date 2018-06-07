using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract class EnemyStateBase
{
    protected List<GameObject> targets = new List<GameObject>();

    protected Finder finder;

    protected GameObject obj { get; set; }

    protected float defaultSpeed;

    abstract public EnemyAI.STATE Update();

    abstract protected void Found(GameObject foundObject);

    abstract protected void Lost(GameObject foundObject);

    public EnemyStateBase(GameObject thisObj, Finder argFinder, float defaultSpeed)
    {
        this.obj = thisObj;
        finder = argFinder;
        this.defaultSpeed = defaultSpeed;
        finder.onFound += OnFound;
        finder.onLost += OnLost;
    }

    public void Destroy()
    {
        finder.onFound -= OnFound;
        finder.onLost -= OnLost;
    }

    private void OnFound(GameObject foundObject)
    {
        targets.Add(foundObject);
        Found(foundObject);
    }

    private void OnLost(GameObject lostObject)
    {
        targets.Remove(lostObject);

        if (targets.Count == 0)
        {
            Lost(lostObject);
        }
    }
}