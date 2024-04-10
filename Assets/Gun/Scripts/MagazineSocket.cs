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

    [SerializeField] XRInteractionManager interactionManager;
    [SerializeField] IXRSelectInteractor socketInteractor;
    [SerializeField] IXRSelectInteractable socketInteractable;

    [SerializeField] Magazine startingMagazine;
    [SerializeField] Gun currentGun;

    private void Awake()
    {
        //collider.gameObject.SetActive(false);
        //if (startingMagazine) CreateStartingMagazine();
        CreateStartingMagazine();
    }

    /*private void OnEnable()
    {
        dropMagazineInput.action.Enable();
    }

    private void OnDisable()
    {
        dropMagazineInput.action.Disable();
    }*/

    private void Start()
    {
        interactionManager = FindObjectOfType<XRInteractionManager>();
        socketInteractor = GetComponent<XRSocketInteractor>();
        currentGun = GetComponentInParent<Gun>();
    }

    public void Update()
    {
        float dropMagazine = dropMagazineInput.action.ReadValue<float>();
        Debug.Log(dropMagazine);
        if (dropMagazine >= 1)
        {
            DropMagazine();
        }
    }

    private void CreateStartingMagazine()
    {
        Debug.Log("CreateStartingMagazine");
        Instantiate(startingMagazine, transform.position, transform.rotation, transform);
    }

    // ÅºÃ¢ ºÐ¸®
    public void DropMagazine()
    {
        Debug.Log("DropMagazine");
        Debug.Log(currentGun.name);

    }

    // ÅºÃ¢ ÀåÂø
    public void InsertMagazine()
    {
        Debug.Log("InsertMagazine");
    }
}
