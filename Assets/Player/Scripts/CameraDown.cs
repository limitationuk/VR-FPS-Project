using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CameraDown : MonoBehaviour
{
    [SerializeField] InputActionReference thumbInput;
    [SerializeField] DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] Transform cameraPos;
    [SerializeField] Vector3 newPosition;
    [SerializeField] bool isDown;
    [SerializeField] bool isDie;


    private void OnEnable()
    {
        thumbInput.action.Enable();
    }

    private void OnDisable()
    {
        thumbInput.action.Disable();
    }


    void LateUpdate()
    {
        if (!isDie)
        {
            float down = thumbInput.action.ReadValue<float>();

        Vector3 currentPosition = cameraPos.parent.position;

        newPosition = cameraPos.localPosition;
        
            if ((newPosition.y > 0.5f) && down == 1)
            {
                dynamicMoveProvider.moveSpeed = 0.5f;
                StartCoroutine(Down());
            }
            else if ((newPosition.y < 1.36144f) && down == 0)
            {
                dynamicMoveProvider.moveSpeed = 1f;
                StartCoroutine(UP());
            }
        }
        

        cameraPos.localPosition = newPosition;

    }



    private IEnumerator Down()
    {
        while (true)
        {
            newPosition.y -= 0.02f;
            yield return new WaitForSeconds(0.1f);

            if (newPosition.y <= 0.5f)
            {  
                newPosition.y = 0.5f;
                break;
            }
        }
    }
    private IEnumerator UP()
    {
        while (true)
        {
            newPosition.y += 0.02f;
            yield return new WaitForSeconds(0.1f);

            if (newPosition.y >= 1.36144f)
            {     
                newPosition.y = 1.36144f;
                break;

            }
        }
    }

    public void DieRoutineStart() 
    {
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        isDie = true;

        while (true)
        {
            Debug.Log("1");
            newPosition.y -= 0.02f;
            yield return new WaitForSecondsRealtime(0.02f);

            if (newPosition.y <= 0.5f)
            {
                
                newPosition.y = 0.5f;

                while (true)
                {
                    newPosition.y += 0.02f;
                    yield return new WaitForSecondsRealtime(0.02f);

                    if (newPosition.y >= 0.6f)
                    {
                        while (true)
                        {
                            newPosition.y -= 0.02f;
                            yield return new WaitForSecondsRealtime(0.02f);
                            if (newPosition.y <= 0.1f)
                            {
                                yield return new WaitForSecondsRealtime(2f);
                                isDie = false;
                                break;
                            }
                        }
                    }
                }

            }
        }
    }
}