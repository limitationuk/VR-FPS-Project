using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] Graber graber;
    [SerializeField] InputActionReference gripInput;
    [SerializeField] InputActionReference triggerInput;
    [SerializeField] InputActionReference thumbInput;

    [SerializeField] Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
        graber = FindAnyObjectByType<Graber>();
    }

    private void OnEnable()
    {
        thumbInput.action.Enable();
    }

    private void OnDisable()
    {
        thumbInput.action.Disable();
    }

    void Update()
    {
        float grip = gripInput.action.ReadValue<float>();
        float trigger = triggerInput.action.ReadValue<float>();
        float thumb = thumbInput.action.ReadValue<float>();
        if (graber.GunGrip)
        {
            animator.SetBool("GunGrip", true);
        }
        else if (graber.ObjectGrip)
        {
            animator.SetBool("ObjectGrip", true);
        }
        else
        {
            animator.SetBool("GunGrip", false);
            animator.SetBool("ObjectGrip", false);
            animator.SetFloat("Grip", grip);
            animator.SetFloat("Trigger", trigger);
            animator.SetFloat("Thumb", thumb);
        }
    }
}
