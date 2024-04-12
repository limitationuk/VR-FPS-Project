using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RightGraber : MonoBehaviour
{
    [SerializeField] bool objectGrip;
    [SerializeField] bool gunGrip;
    [SerializeField] XRInteractionManager manager;
    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor directInteractor;
    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor socketInteractor;
    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable directInteractable;
    

    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable socketInteractable;
    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable socketInteractable2;
    [SerializeField] bool sceneChange;
    [SerializeField] bool socketTrigger;



    public XRInteractionManager Manager => manager;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor DirectInteractor { get => directInteractor; set => directInteractor = value; } 
    public UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor SocketInteractor { get => socketInteractor; set => socketInteractor = value; }
    
    public UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable DirectInteractable { get => directInteractable; set => directInteractable = value; }

    public UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable SocketInteractable { get => socketInteractable; set => socketInteractable = value; }
    public bool SceneChange { get => sceneChange; set => sceneChange = value; }
    public bool SocketTrigger { get => socketTrigger; set => socketTrigger = value; }


    public bool ObjectGrip => objectGrip;
    public bool GunGrip => gunGrip;

    private void Start()
    {
        directInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor>();
        socketInteractor = FindAnyObjectByType<BodySocketInventory>();
    }


    public void Grab(SelectEnterEventArgs args)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject;
        directInteractable = interactable;

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

        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject;

        if (SocketTrigger && !sceneChange)
        {
            if (socketInteractable == null)
            {
                socketInteractable = directInteractable;
                directInteractable = null;
            }
            else if (socketInteractable != null)
            {
                socketInteractable2 = socketInteractable;
                socketInteractable = directInteractable;
                directInteractable = socketInteractable2;
                manager.SelectEnter(socketInteractor, directInteractable);
                manager.SelectEnter(directInteractor, socketInteractable2);
            }

        }
        else if (!SocketTrigger && !sceneChange)
        {
            directInteractable = null;
        }



        if (gunGrip && ((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)// (interactable.transform.gameObject.layer == InteractionLayerMask.NameToLayer("Gun"))
        {
            gunGrip = false;
        }
        else if (objectGrip)
        {
            objectGrip = false;
        }
    }

    
}
