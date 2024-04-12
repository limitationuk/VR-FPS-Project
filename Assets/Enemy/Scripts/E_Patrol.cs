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
        // 현재 목표 지점 인덱스 증가
        enemyState.Agent.SetDestination(enemyState.WayPoints[enemyState.CurrentWayPointIndex].position);
        // 모든 목표 지점을 돌았다면 처음으로 
        //if (enemyState.Agent.remainingDistance <= enemyState.Agent.stoppingDistance)
        //
            

       

            // 다음 목표 지점 설정
      //  }


    }
}
