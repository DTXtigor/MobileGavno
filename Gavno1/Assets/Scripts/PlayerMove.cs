using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public JoystickMovment joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;

    void Start()
    {
        controller= GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Vector3 Move = transform.right * joystick._InputVector.x + transform.forward * joystick._InputVector.y;
        controller.Move(Move * SpeedMove * Time.deltaTime);
    }
}
