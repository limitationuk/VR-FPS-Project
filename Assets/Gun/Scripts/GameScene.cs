using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] PooledObject hitEffectPrefab;

    private void Start()
    {
        Manager.Pool.CreatePool(hitEffectPrefab, 5, 5);
    }
}
