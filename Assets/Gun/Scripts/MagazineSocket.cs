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

        socketInteractor = GetComponent<XRSocketInteractor>(); // MagazineSocket (����)

        curGun = GetComponentInParent<Gun>(); // Pistol (����)

        curMagazine = GameObject.Find($"{curGun.name}Magazine"); // PistolMagazine (źâ : �����̸�+Magazine)

        magazineCollider = curMagazine.GetComponent<Collider>(); // PistolMagazineModel (źâ��)
        Debug.Log($"111{magazineCollider}");

        socketInteractable = curMagazine.GetComponent<IXRSelectInteractable>(); // PistolMagazine (źâ)

        magazineCollider.enabled = false;
        Debug.Log($"{socketInteractor}");
    }

    public void Update()
    {
        // A ��ư�� ������ ��
        float dropMagazine = dropMagazineInput.action.ReadValue<float>();
        if (dropMagazine >= 1 && socketInteractable != null)
        {
            Debug.Log(socketInteractable);
            DropMagazine();
        }
    }

    // źâ �и� �ڷ�ƾ
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

    // źâ �и�
    public void DropMagazine()
    {
        if (!rightGraber.GunGrip)
            return;

        StartCoroutine(DropMagazineRoutine());

        //socketInteractable = null;
    }

    // źâ ���� �ڷ�ƾ
    /*IEnumerator InsertMagazineRoutine()
    {
        socketInteractor.socketActive = false;

        yield return new WaitForSeconds(0.2f);

        magazineCollider.enabled = true;
        curMagazine.transform.SetParent(null);

        yield return new WaitForSeconds(1.0f);

        socketInteractor.socketActive = true;
    }*/

    // źâ ����
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
