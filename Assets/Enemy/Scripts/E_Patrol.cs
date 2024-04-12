using UnityEngine;
using static EnemyState;

public class E_Patrol : EnemyStateData
{


    public E_Patrol(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    public override void Enter()
    {
        enemyState.Animator.SetBool("patrol", true);
        SetNextWayPoint();

   
    }

    public override void Update()
    {
        //enemyState.Agent.SetDestination(enemyState.WayPoints[Random.Range(0, enemyState.WayPoints.Count)].position);
    }

    public override void Exit()
    {
        enemyState.Animator.SetBool("patrol", false);
        enemyState.Agent.SetDestination(enemyState.Agent.transform.position);
        enemyState.CurrentWayPointIndex++;
    }

    public override void Transition()
    {
        if (enemyState.Agent.remainingDistance <= enemyState.Agent.stoppingDistance)
        {
            ChangeState(EnemyState.State.E_Idle);
        }
    }

    private void SetNextWayPoint()
    {
        // ���� ��ǥ ���� �ε��� ����
        enemyState.Agent.SetDestination(enemyState.WayPoints[enemyState.CurrentWayPointIndex].position);
        // ��� ��ǥ ������ ���Ҵٸ� ó������ 
        //if (enemyState.Agent.remainingDistance <= enemyState.Agent.stoppingDistance)
        //
            

       

            // ���� ��ǥ ���� ����
      //  }


    }
}
