using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSocket : MonoBehaviour
{
    [Tooltip("")]

    [SerializeField] InputActionReference dropMagazineInput;

    //[SerializeField] XRInteractionManager manager;
    //[SerializeField] IXRSelectInteractor socketInteractor; // MagazineSocket
    [SerializeField] IXRSelectInteractable socketInteractable; // PistolMagazine
    [SerializeField] XRSocketInteractor socketInteractor; // MagazineSocket

    [SerializeField] RightGraber rightGraber;

    Gun curGun;
    GameObject curMagazine;
    Collider magazineCollider;

    //public XRInteractionManager Manager => manager;
    //public IXRSelectInteractor SocketInteractor { get => socketInteractor; set => socketInteractor = value; }
    //public IXRSelectInteractable SocketInteractable { get => socketInteractable; set => socketInteractable = value; }

    private void OnEnable()
    {
        dropMagazineInput.action.Enable();
    }

    private void OnDisable()
    {
        dropMagazineInput.action.Disable();
    }

    private void Start()
    {
        //manager = FindAnyObjectByType<XRInteractionManager>();

        socketInteractor = GetComponent<XRSocketInteractor>(); // MagazineSocket (¼ÒÄÏ)

        curGun = GetComponentInParent<Gun>(); // Pistol (¹«±â)

        curMagazine = GameObject.Find($"{curGun.name}Magazine"); // PistolMagazine (ÅºÃ¢ : ¹«±âÀÌ¸§+Magazine)

        magazineCollider = curMagazine.GetComponent<Collider>(); // PistolMagazineModel (ÅºÃ¢¸ðµ¨)
        Debug.Log($"111{magazineCollider}");

        socketInteractable = curMagazine.GetComponent<IXRSelectInteractable>(); // PistolMagazine (ÅºÃ¢)

        magazineCollider.enabled = false;
        Debug.Log($"{socketInteractor}");
    }

    public void Update()
    {
        // A ¹öÆ°À» ´­·¶À» ¶§
        float dropMagazine = dropMagazineInput.action.ReadValue<float>();
        if (dropMagazine >= 1 && socketInteractable != null)
        {
            Debug.Log(socketInteractable);
            DropMagazine();
        }
    }

    // ÅºÃ¢ ºÐ¸® ÄÚ·çÆ¾
    IEnumerator DropMagazineRoutine()
    {
        magazineCollider.enabled = false;

        socketInteractor.socketActive = false;

        yield return new WaitForSeconds(0.2f);

        magazineCollider.enabled = true;
        curMagazine.transform.SetParent(null);

        yield return new WaitForSeconds(1.0f);

        socketInteractor.socketActive = true;
    }

    // ÅºÃ¢ ºÐ¸®
    public void DropMagazine()
    {
        if (!rightGraber.GunGrip)
            return;

        StartCoroutine(DropMagazineRoutine());

        //socketInteractable = null;
    }

    // ÅºÃ¢ ÀåÂø ÄÚ·çÆ¾
    /*IEnumerator InsertMagazineRoutine()
    {
        socketInteractor.socketActive = false;

        yield return new WaitForSeconds(0.2f);

        magazineCollider.enabled = true;
        curMagazine.transform.SetParent(null);

        yield return new WaitForSeconds(1.0f);

        socketInteractor.socketActive = true;
    }*/

    // ÅºÃ¢ ÀåÂø
    public void InsertMagazine()
    {
        if (!rightGraber.GunGrip)
            return;
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("hoverEnter");
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("hoverExit");
    }

    /*public void OnSelectEnter(SelectEnterEventArgs args)
    {
        Debug.Log("selectEnter");
        //IXRSelectInteractable interactable = args.interactableObject;
        //Debug.Log(interactable);


    }*/
}
