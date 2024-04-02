using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightGraber : MonoBehaviour
{
    [SerializeField] bool objectGrip;
    [SerializeField] bool gunGrip;

    public bool ObjectGrip => objectGrip;
    public bool GunGrip => gunGrip;
    
    public void Grab(SelectEnterEventArgs args)
    {
        IXRInteractable interactable = args.interactableObject;


        if (((1<<interactable.interactionLayers) & (1<<InteractionLayerMask.GetMask("Gun"))) != 0)//interactable.transform.gameObject.name == "Gun"
        {                        //비트마스크 
            gunGrip = true;
            Debug.Log(1 );
        }
        else
        {
            objectGrip = true;
            Debug.Log(2);
        }
    }
  

    public void UnGrab(SelectExitEventArgs args)
    {
        IXRInteractable interactable = args.interactableObject;
        if (((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)// (interactable.transform.gameObject.layer == InteractionLayerMask.NameToLayer("Gun"))
        {
            gunGrip = false;

        }
        else
        {
            objectGrip = false;
        }
    }


}
