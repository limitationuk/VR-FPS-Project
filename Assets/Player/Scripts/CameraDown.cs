using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CameraDown : MonoBehaviour
{
    [SerializeField] InputActionReference thumbInput;
    public DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] Transform cameraPos;
    [SerializeField] Vector3 newPosition;
    [SerializeField] bool isDown;
    [SerializeField] bool isDie;
    [SerializeField] Coroutine dieCoroutine;


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
                StartCoroutine(Down());
            }
            else if ((newPosition.y < 1.36144f) && down == 0)
            {
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
                dynamicMoveProvider.moveSpeed = 1f;
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
                dynamicMoveProvider.moveSpeed = 3f;
                newPosition.y = 1.36144f;
                break;

            }
        }
    }

    public void DieRoutineStart() 
    {
        dieCoroutine = StartCoroutine(DieRoutine());
    }

    public void DieRoutineStop()
    {
        StopCoroutine(dieCoroutine);
    }

    IEnumerator DieRoutine()
    {
        isDie = true;

        while (true)
        {
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
                                isDie = false;
                                DieRoutineStop();
                                newPosition.y = 1.36144f;
                                break;
                            }
                        }
                    }
                }

            }
        }
    }
}
