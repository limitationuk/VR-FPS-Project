using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    [SerializeField] Transform playerPos;
    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos.transform.position = transform.position;
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
