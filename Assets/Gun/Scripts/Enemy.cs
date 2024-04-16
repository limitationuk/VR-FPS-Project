using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;

    //public int Hp => hp;  // 읽기 전용
    public int Hp { get => hp; set => hp = value; }  // 읽기 ok, 쓰기 ok

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
