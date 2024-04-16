using UnityEngine;
using static EnemyState;

public class E_Shooting : EnemyStateData
{
    [SerializeField] float distance;

    public E_Shooting(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }
    public override void Enter()
    {
        enemyState.Animator.SetBool("shooting", true);
    }

    public override void Update()
    {
        enemyState.transform.LookAt(enemyState.Player);
        Vector3 eulerAngles = enemyState.transform.eulerAngles;
        eulerAngles.x = 0f;
        enemyState.transform.eulerAngles = eulerAngles;
        distance = Vector3.Distance(enemyState.transform.position, enemyState.Player.position);
    }

    public override void Exit()
    {
        enemyState.Animator.SetBool("shooting", false);
    }
    public override void Transition()
    {
        if (distance > 15)
        {
            ChangeState(EnemyState.State.E_Chase);
        }
    }

}
