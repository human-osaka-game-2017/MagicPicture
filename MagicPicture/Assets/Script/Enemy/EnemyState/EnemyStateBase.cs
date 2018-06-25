using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract class EnemyStateBase
{
    private List<GameObject> targets = new List<GameObject>();
    protected List<GameObject> Targets
    {
        get { return targets; }
    }

    protected Finder Finder { get; set; }

    protected GameObject Obj { get; set; }

    protected NavMeshAgent NavAgent { get; set; }

    abstract public EnemyAI.STATE Update();

    abstract protected void Found(GameObject foundObject);

    abstract protected void Lost(GameObject foundObject);

    abstract public void Collision(GameObject other);

    public EnemyStateBase(GameObject obj, Finder finder)
    {
        this.Obj = obj;
        this.NavAgent = this.Obj.GetComponent<NavMeshAgent>();
        this.Finder = finder;
        this.Finder.onFound += OnFound;
        this.Finder.onLost += OnLost;
    }

    public void Destroy()
    {
        this.Finder.onFound -= OnFound;
        this.Finder.onLost -= OnLost;
    }

    private void OnFound(GameObject foundObject)
    {
        this.Targets.Add(foundObject);
        Found(foundObject);
    }

    private void OnLost(GameObject lostObject)
    {
        this.Targets.Remove(lostObject);

        if (this.Targets.Count == 0)
        {
            Lost(lostObject);
        }
    }
}