using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Stage01Scene spawn;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Body")) != 0)
        {
           
            spawn = FindAnyObjectByType<Stage01Scene>();
            spawn.transform.position = transform.position;
            
        }
    }
 
}
