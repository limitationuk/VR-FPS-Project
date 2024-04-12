using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class bodySocket
{
    public GameObject gameObject;
    [Range(0.01f, 1f)]
    public float heightRatio;
}

public class BodySocketInventory : UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor

{
    [SerializeField] RightGraber rightGraber;
    public GameObject HMD;
    public bodySocket[] bodySockets;

    private Vector3 _currentHMDlocalPosition;
    private Quaternion _currentHMDRotation;
    void Update()
    {
        _currentHMDlocalPosition = HMD.transform.localPosition;
        _currentHMDRotation = HMD.transform.rotation;
        foreach(var bodySocket in bodySockets)
        {
            UpdateBodySocketHeight(bodySocket);
        }
        UpdateSocketInventory();
    }

    private void UpdateBodySocketHeight(bodySocket bodySocket)
    {

        bodySocket.gameObject.transform.localPosition = new Vector3(bodySocket.gameObject.transform.localPosition.x,(_currentHMDlocalPosition.y * bodySocket.heightRatio), bodySocket.gameObject.transform.localPosition.z);
    }

    private void UpdateSocketInventory()
    {
        //transform.localPosition = new Vector3(_currentHMDlocalPosition.x, 0, _currentHMDlocalPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentHMDRotation.y, transform.rotation.z, _currentHMDRotation.w);
    }


    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable;

        if (((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)
        {
            rightGraber.SocketTrigger = true;
            Debug.Log(rightGraber.SocketTrigger);
        }
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable;

        if (((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)
        {
            
            rightGraber.SocketTrigger = false;
            Debug.Log(rightGraber.SocketTrigger);
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (((1 << collision.gameObject.layer) & LayerMask.GetMask("Gun")) != 0)
    //    {
    //        rightGraber.SocketTrigger = true;
    //    }
    //}
    //private void OnTriggerExit(Collider collision)
    //{
    //    if (((1 << collision.gameObject.layer) & LayerMask.GetMask("Gun")) != 0)
    //    {
    //        rightGraber.SocketTrigger = false;
    //    }
    //}
}
