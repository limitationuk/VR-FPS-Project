using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnemyState;

public class E_Hit : EnemyStateData
{

    public E_Hit(EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    public override void Enter()
    {
        enemyState.Animator.SetTrigger("hit");
    }
    public override void Update()
    {
   

    }

}
