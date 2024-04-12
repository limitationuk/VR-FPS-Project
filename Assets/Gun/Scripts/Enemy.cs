using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;
    [SerializeField] EnemyState enemyState;
    [SerializeField] Animator animator;


    //public int Hp => hp;  // �б� ����
    public int Hp { get => hp; set => hp = value; }  // �б� ok, ���� ok

    private void Die()
    {
        animator.enabled = false;
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = false;
        }
        enemyState.stateMachine.ChangeState(EnemyState.State.E_Die);

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            //enemyState.stateMachine.ChangeState(EnemyState.State.E_Hit); 
            enemyState.Animator.SetTrigger("hit");

        }
    }
}
