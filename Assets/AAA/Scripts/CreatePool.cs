using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePool : MonoBehaviour
{
    [SerializeField] PooledObject hitEffectPrefab;

    private void Start()
    {
        Manager.Pool.CreatePool(hitEffectPrefab, 20, 20);
    }
}
