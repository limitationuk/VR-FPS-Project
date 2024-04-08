using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunInteractor : MonoBehaviour
{
    [SerializeField] Collider col;

    public void colliderOff()
    {
        StartCoroutine(colliderOffRoutine());
    }
    public void DontDestroy()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator colliderOffRoutine()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }


}
