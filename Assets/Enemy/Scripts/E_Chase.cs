using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyState;

public class E_Chase : EnemyStateData
{
    [SerializeField] float distance;
    public E_Chase(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    public override void Enter()
    {
        enemyState.Animator.SetBool("chase",true);
    }

    public override void Update() 
    {
        enemyState.Agent.SetDestination(enemyState.Player.position);
        distance = Vector3.Distance(enemyState.transform.position, enemyState.Player.position);
    }

    public override void Exit()
    {
        enemyState.Animator.SetBool("chase", false);
        enemyState.Agent.SetDestination(enemyState.Agent.transform.position);
    }


    public override void Transition()
    {
        if (distance < 10)
        {
            ChangeState(EnemyState.State.E_Shooting);
        }
    }
    

       
}
