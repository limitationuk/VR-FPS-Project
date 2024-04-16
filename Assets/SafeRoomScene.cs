using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomScene : BaseScene
{
    [SerializeField] Transform playerPos;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos.transform.position = transform.position;
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
