//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] int regenCool;
    [SerializeField] int regenDelay;
    [SerializeField] bool regen;
    [SerializeField] float percent;
    [SerializeField] UnityEngine.UI.Image bloodScreen;
    [SerializeField] Coroutine coroutine;
    [SerializeField] GameObject eye;
    [SerializeField] GameObject blur;
    [SerializeField] GameObject Move;
    [SerializeField] GameObject Teleport;
    [SerializeField] CameraDown cameraDown;
    [SerializeField] CharacterController characterController;


    private void Update()
    {
        if (0 < hp && hp < maxHp)
        {
            bloodScreen.color = new Color(1, 0, 0, 1f / hp);
        } 
    }

    public void TakeDamage()
    {
        if (Random.value < percent)
        {
            if (regen)
            {
                hp--;
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(BloodScreen());
            }
            else
            {
                hp--;
                coroutine = StartCoroutine(BloodScreen());
            }

            if (hp <= 0)
            {
                Die();
            }
        }
       
    }


    
    private void Die()
    {
        eye.SetActive(true);
        blur.SetActive(true);
        Move.SetActive(false);
        Teleport.SetActive(false);
        characterController.enabled = false;
        cameraDown.DieRoutineStart();
    }


    IEnumerator BloodScreen()
    {
        regen = true;
        yield return new WaitForSeconds(regenCool);
        while (true)
        {
            hp++;
            yield return new WaitForSeconds(regenDelay);
            if (hp >= maxHp) 
            {
                bloodScreen.color = Color.clear;
                regen = false;
                break;
            }
        }
       
    }
}
