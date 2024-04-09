using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSocket : MonoBehaviour
{
    [Tooltip("")]

    [SerializeField] XRInteractionManager interactionManager;
    [SerializeField] IXRSelectInteractor socketInteractor;
    [SerializeField] IXRSelectInteractable socketInteractable;

    [SerializeField] Magazine startingMagazine;

    private void Awake()
    {
        //collider.gameObject.SetActive(false);
        //if (magazine.startingMagazine) CreateStartingMagazine();
        CreateStartingMagazine();
    }

    private void CreateStartingMagazine()
    {
        Debug.Log("CreateStartingMagazine");
        Instantiate(startingMagazine, transform.position, transform.rotation, transform);
    }

    public void DropMagazine(SelectExitEventArgs args)
    {
    }

    // źâ �и�
    public void MagazineSelectExit()
    {
        Debug.Log("MagazineSelectExit");
    }

    // źâ ����
    public void MagazineSelectEnter()
    {
        Debug.Log("MagazineSelectEnter");
    }
}
