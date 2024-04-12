using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySocket : UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor
{
    [SerializeField] InputActionProperty selectInput;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.IXRInteractable currentObject; //���� ������Ʈ
    private UnityEngine.XR.Interaction.Toolkit.Interactables.IXRInteractable deselectObject; //

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

    public override bool CanSelect(UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable)
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

    public void HoverEnter(HoverEnterEventArgs args) //ȣ�� ����
    {
        if (args.interactableObject == currentObject) //�׸� ������Ʈ == ���� ������Ʈ �� ��
            return;

        hovering = true;
    }

    public void HoverExit(HoverExitEventArgs args) //ȣ�� ����
    {
        hovering = false;
    }

    public void SelectEnter(SelectEnterEventArgs args) //�׸� ������Ʈ -> ���� ������Ʈ
    {
        currentObject = args.interactableObject;
    }

    public void SelectExit(SelectExitEventArgs args) //���� ������Ʈ ����
    {
        currentObject = null;
    }
}