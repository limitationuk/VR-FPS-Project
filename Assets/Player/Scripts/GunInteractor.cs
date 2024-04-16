using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunInteractor : MonoBehaviour
{


    public void DontDestroy()
    {
        DontDestroyOnLoad(this.gameObject);
    }




}
