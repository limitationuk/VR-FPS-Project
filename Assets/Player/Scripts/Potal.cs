using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{

    [SerializeField] string scenename;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Player")) != 0)
        {
            Manager.Scene.LoadScene(scenename);
        }
    }
}
