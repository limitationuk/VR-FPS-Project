using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01Scene : BaseScene
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
