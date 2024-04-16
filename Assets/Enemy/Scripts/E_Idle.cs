using UnityEngine;
using static EnemyState;

public class E_Idle : EnemyStateData
{
    [SerializeField] float timer;
    [SerializeField] bool patrolAfter;
    public E_Idle(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    public override void Enter()
    {
        timer = 0;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

    }

    public override void Transition()
    {
        if (patrolAfter && timer > enemyState.WaitTime)
        {
            ChangeState(EnemyState.State.E_Rotating);
            patrolAfter = false;
        }
        else if (!patrolAfter && timer > enemyState.WaitTime)
        {
            ChangeState(EnemyState.State.E_Patrol);
            patrolAfter = true;
        }


    }
}

