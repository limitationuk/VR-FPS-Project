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
        //������ ���� �ٴٸ��� ó������ �ʱ�ȭ
        if (enemyState.CurrentWayPointIndex >= enemyState.WayPoints.Count)
        {
            enemyState.CurrentWayPointIndex = 0;
        }
    
        RotateTowardsWaypoint();

    }

    public override void Update()
    {
        //���� �������� ȸ��
        enemyState.transform.rotation = Quaternion.RotateTowards(enemyState.transform.rotation, targetRotation, enemyState.Agent.angularSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        //�ִϸ��̼� ����
        enemyState.Animator.SetBool("LeftTurn", false);
        enemyState.Animator.SetBool("RightTurn", false);
    }

    public override void Transition()
    {
        //������ 1�̸��� �� ������
        if (Quaternion.Angle(enemyState.transform.rotation, targetRotation) < 5f)
        {
            ChangeState(EnemyState.State.E_Idle);
        }
    }

    private void RotateTowardsWaypoint()
    {
        //���� ������ ȸ�� ���
        targetDirection = enemyState.WayPoints[enemyState.CurrentWayPointIndex].position - enemyState.transform.position;

        targetRotation = Quaternion.LookRotation(targetDirection);

        //ȸ������ �Ǵ� or �ִϸ��̼�
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
