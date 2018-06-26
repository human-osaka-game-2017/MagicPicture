using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashRotSpeed;
    [SerializeField] private float breakInterval;
    [SerializeField] private int actionNum;
    [SerializeField] private GameObject target;

    public enum STATE
    {
        SEARCH,     //探索
        BREAKTIME,  //休息
        ATTACK      //攻撃
    }

    private STATE currentStateId = STATE.SEARCH;
    private STATE prevStateId = STATE.SEARCH;
    private EnemyStateBase state;
    private EnemyStateFactory factory;

    private void Start()
    {
        this.factory = new EnemyStateFactory(this.gameObject, this.GetComponentInChildren<Finder>(),
            this.dashSpeed,
            this.dashRotSpeed,
            this.breakInterval, this.actionNum);

        this.state = this.factory.Create(this.currentStateId);
    }

    private void Update()
    {
        this.currentStateId = this.state.Update();
        if (this.currentStateId != this.prevStateId)
        {
            this.state = this.factory.Create(currentStateId);
        }

        this.prevStateId = this.currentStateId;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == target)
        {
            SoundManager.GetInstance().Play("SE_EnemyHit", SoundManager.PLAYER_TYPE.NONLOOP, true);
        }
        this.state.Collision(other.gameObject);
    }
}