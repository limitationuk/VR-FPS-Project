using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyState;

public class E_Rotating : EnemyStateData
{
    [SerializeField] Quaternion targetRotation;
    [SerializeField] Vector3 targetDirection;
    public E_Rotating(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }
    public override void Enter()
    {
        //목적지 끝에 다다르면 처음으로 초기화
        if (enemyState.CurrentWayPointIndex >= enemyState.WayPoints.Count)
        {
            enemyState.CurrentWayPointIndex = 0;
        }
    
        RotateTowardsWaypoint();

    }

    public override void Update()
    {
        //다음 목적지로 회전
        enemyState.transform.rotation = Quaternion.RotateTowards(enemyState.transform.rotation, targetRotation, enemyState.Agent.angularSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        //애니메이션 끄기
        enemyState.Animator.SetBool("LeftTurn", false);
        enemyState.Animator.SetBool("RightTurn", false);
    }

    public override void Transition()
    {
        //간격이 1미만일 때 대기상태
        if (Quaternion.Angle(enemyState.transform.rotation, targetRotation) < 5f)
        {
            ChangeState(EnemyState.State.E_Idle);
        }
    }

    private void RotateTowardsWaypoint()
    {
        //다음 목적지 회전 계산
        targetDirection = enemyState.WayPoints[enemyState.CurrentWayPointIndex].position - enemyState.transform.position;

        targetRotation = Quaternion.LookRotation(targetDirection);

        //회전방향 판단 or 애니메이션
        Vector3 forward = enemyState.transform.forward;

        float angle = Vector3.SignedAngle(forward, targetDirection, Vector3.up);

        if (angle < 0)
        {
            enemyState.Animator.SetBool("LeftTurn", true);
        }
        else
        {
            enemyState.Animator.SetBool("RightTurn", true);
        }
    }

  
}
