using System;
using System.Collections;
using UnityEngine;

public class StartRoomScene : BaseScene
{

    [SerializeField] PooledObject hitEffectPrefab;
    private void Start()
    {
        Manager.Pool.CreatePool(hitEffectPrefab, 20, 20);
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

}