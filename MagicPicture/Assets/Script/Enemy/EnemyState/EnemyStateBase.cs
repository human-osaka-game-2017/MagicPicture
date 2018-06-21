using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

abstract class EnemyStateBase
{
    protected List<GameObject> targets = new List<GameObject>();

    protected Finder finder;

    protected GameObject obj { get; set; }

    protected NavMeshAgent navMeshAgent;

    abstract public EnemyAI.STATE Update();

    abstract protected void Found(GameObject foundObject);

    abstract protected void Lost(GameObject foundObject);

    abstract public void Collision(GameObject other);

    public EnemyStateBase(GameObject thisObj, Finder argFinder)
    {
        this.obj = thisObj;
        this.navMeshAgent = this.obj.GetComponent<NavMeshAgent>();
        this.finder = argFinder;
        this.finder.onFound += OnFound;
        this.finder.onLost += OnLost;
    }

    public void Destroy()
    {
        this.finder.onFound -= OnFound;
        this.finder.onLost -= OnLost;
    }

    private void OnFound(GameObject foundObject)
    {
        this.targets.Add(foundObject);
        Found(foundObject);
    }

    private void OnLost(GameObject lostObject)
    {
        this.targets.Remove(lostObject);

        if (this.targets.Count == 0)
        {
            Lost(lostObject);
        }
    }
}