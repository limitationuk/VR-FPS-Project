using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomScene : BaseScene
{
    [SerializeField] Transform playerPos;
    [SerializeField] PooledObject hitEffectPrefab;

    private void Start()
    {
        Manager.Pool.CreatePool(hitEffectPrefab, 20, 20);
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos.transform.position = transform.position;
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
