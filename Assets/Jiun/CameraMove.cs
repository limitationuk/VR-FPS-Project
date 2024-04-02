using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    
    public float moveSpeed = .1f;
    public void move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed, 0, 0);

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, moveSpeed);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -moveSpeed);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, -30, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, 30, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
