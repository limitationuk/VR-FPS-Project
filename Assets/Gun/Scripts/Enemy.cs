using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;

    //public int Hp => hp;  // �б� ����
    public int Hp { get => hp; set => hp = value; }  // �б� ok, ���� ok

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }
}
