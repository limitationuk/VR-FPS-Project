using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Graber : MonoBehaviour
{
    [SerializeField] bool objectGrip;
    [SerializeField] bool gunGrip;
    [SerializeField] XRInteractionManager manager;
    [SerializeField] IXRSelectInteractor directInteractor;
    [SerializeField] IXRSelectInteractable directInteractable;
    [SerializeField] bool sceneChange;

    public XRInteractionManager Manager => manager;
    public IXRSelectInteractor DirectInteractor { get => directInteractor; set => directInteractor = value; }
    public IXRSelectInteractable DirectInteractable { get => directInteractable; set => directInteractable = value; }
    public bool SceneChange { get => sceneChange; set => sceneChange = value; }
    //[SerializeField] Collider interactableCollider;
    private void Start()
    {
        directInteractor = GetComponent<XRDirectInteractor>();
    }

    public bool ObjectGrip => objectGrip;
    public bool GunGrip => gunGrip;

    public void Grab(SelectEnterEventArgs args)
    {

        IXRSelectInteractable interactable = args.interactableObject;
        directInteractable = interactable;


        if (!gunGrip && ((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)//interactable.transform.gameObject.name == "Gun"
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
        IXRSelectInteractable interactable = args.interactableObject;

        if (!sceneChange)
        {
            directInteractable = null;

        }


        // IXRInteractable interactable = args.interactableObject;
        //GameObject interactableGameObject = interactable.colliders[0].gameObject;

        //interactableCollider = interactableGameObject.gameObject.GetComponent<Collider>();
        //StartCoroutine(ColOff());

        if (gunGrip && ((1 << interactable.interactionLayers) & (1 << InteractionLayerMask.GetMask("Gun"))) != 0)// (interactable.transform.gameObject.layer == InteractionLayerMask.NameToLayer("Gun"))
        {
            gunGrip = false;
        }
        else if (objectGrip)
        {
            objectGrip = false;
        }
    }


    //IEnumerator ColOff()
    //{
    //    interactableCollider.enabled = false;
    //    yield return new WaitForSeconds(0.5f);
    //    interactableCollider.enabled = true;
    //}

    

}

