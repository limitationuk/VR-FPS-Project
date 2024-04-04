using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{

    [SerializeField] string scenename;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1 << other.gameObject.layer);
        Debug.Log(LayerMask.GetMask("Player"));
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Player")) != 0)
        {
            Debug.Log("1");
            Manager.Scene.LoadScene(scenename);
        }
    }
}
