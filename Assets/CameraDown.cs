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

        Vector3 currentPosition = cameraPos.position;

        // y���� �����Ͽ� ���ο� ��ġ ���͸� ����
        Vector3 newPosition = new Vector3(currentPosition.x, 0.7f, currentPosition.z);

        if (down == 1)
        {
            cameraPos.position = newPosition;
            Debug.Log(down);
        }
        else
        {
            cameraPos.position = new Vector3(currentPosition.x, 1.36144f, currentPosition.z);
        }
        
        
    }
}
