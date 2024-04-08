using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestScene : BaseScene
{
    [SerializeField] Transform playerPos;
    [SerializeField] Graber graber;
    [SerializeField] RightGraber rightGraber;
   
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos.transform.position = transform.position;

        //graber = GameObject.FindObjectOfType<Graber>();
        //rightGraber = GameObject.FindObjectOfType<RightGraber>();
        
        //if (rightGraber.DirectInteractable != null)
        //{
        //    rightGraber.Manager.SelectEnter(rightGraber.DirectInteractor, rightGraber.DirectInteractable);
        //} 

        
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
