using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int hp = 5;

    public void TakeDamage()
    {
        hp--;
        

        if (hp <= 0)
        {
            Die();
        }

    }
    public void Die()
    {

    }
}
