using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDown : MonoBehaviour
{
    [SerializeField] InputActionReference xInput;
    [SerializeField] Transform cameraPos;


 
    void Update()
    {
        float down = xInput.action.ReadValue<float>();

        Vector3 currentPosition = cameraPos.parent.position;

        // y값만 조정하여 새로운 위치 벡터를 생성
        Vector3 newPosition = cameraPos.localPosition;

        if (down == 1)
        {
            newPosition.y = 0.7f;
            Debug.Log(down);
        }
        else
        {
            newPosition.y = 1.36144f;
        }

        cameraPos.localPosition = newPosition;
    }
}
