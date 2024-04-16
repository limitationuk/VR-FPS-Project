using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AreaOnTriggerCheck : MonoBehaviour
{
    [SerializeField] bool shootAreaOnTrigger;
    [SerializeField] bool moveAreaOnTrigger;

    [SerializeField] Gun gun;
    [SerializeField] GameObject rightController;
    [SerializeField] Transform[] rightHand;

    void Start()
    {


    }

    void Update()
    {
        
    }
}
