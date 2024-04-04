using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySocket : XRSocketInteractor
{
    [SerializeField] InputActionProperty selectInput;

    private IXRInteractable currentObject; //현재 오브젝트
    private IXRInteractable deselectObject; //

    private bool hovering;

    protected override void OnEnable()
    {
        base.OnEnable();

        selectInput.action.performed += OnSelect;
        selectInput.action.canceled += OnCancel;
        hoverEntered.AddListener(HoverEnter);
        hoverExited.AddListener(HoverExit);
        selectEntered.AddListener(SelectEnter);
        selectExited.AddListener(SelectExit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        selectInput.action.performed -= OnSelect;
        selectInput.action.canceled -= OnCancel;
        hoverEntered.RemoveListener(HoverEnter);
        hoverExited.RemoveListener(HoverExit);
        selectEntered.RemoveListener(SelectEnter);
        selectExited.RemoveListener(SelectExit);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable != deselectObject;
    }

    public void OnSelect(InputAction.CallbackContext context)
    {

    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (currentObject != null && hovering == true)
        {
            StartCoroutine(SwapRoutine());
        }
    }

    IEnumerator SwapRoutine() //
    {
        deselectObject = currentObject;
        yield return null;
        deselectObject = null;
    }

    public void HoverEnter(HoverEnterEventArgs args) //호버 시작
    {
        if (args.interactableObject == currentObject) //그립 오브젝트 == 현재 오브젝트 일 떄
            return;

        hovering = true;
    }

    public void HoverExit(HoverExitEventArgs args) //호버 끄기
    {
        hovering = false;
    }

    public void SelectEnter(SelectEnterEventArgs args) //그립 오브젝트 -> 현재 오브젝트
    {
        currentObject = args.interactableObject;
    }

    public void SelectExit(SelectExitEventArgs args) //현재 오브젝트 비우기
    {
        currentObject = null;
    }
}