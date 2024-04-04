using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightGraber : MonoBehaviour
{
    [SerializeField] bool objectGrip;
    [SerializeField] bool gunGrip;
    [SerializeField] Collider interactableCollider;

    public bool ObjectGrip => objectGrip;
    public bool GunGrip => gunGrip;
    
    public void Grab(SelectEnterEventArgs args)
    {
        IXRInteractable interactable = args.interactableObject;


        if (!gunGrip && ((1<<interactable.interactionLayers) & (1<<InteractionLayerMask.GetMask("Gun"))) != 0)//interactable.transform.gameObject.name == "Gun"
        {                        
            gunGrip = true;
        }
        else if (!objectGrip)
        {
            objectGrip = true;
        }
    }


    public void UnGrab(SelectExitEventArgs args)
    {
        IXRInteractable interactable = args.interactableObject;
        GameObject interactableGameObject = interactable.colliders[0].gameObject;
        interactableCollider = interactableGameObject.gameObject.GetComponent<Collider>();
        StartCoroutine(ColOff());

        if (gunGrip && ((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)// (interactable.transform.gameObject.layer == InteractionLayerMask.NameToLayer("Gun"))
        {
            gunGrip = false;
        }
        else if (objectGrip)
        {
            objectGrip = false;
        }
    }
    IEnumerator ColOff()
    {
        interactableCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        interactableCollider.enabled = true;
    }

}
