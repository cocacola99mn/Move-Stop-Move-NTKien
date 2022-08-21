using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public CharacterController controller;

    public Joystick joyStick;

    float horizontal, vertical;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        PlayerCircleCast();
        PlayerAction();
    }

    public void PlayerAction()
    {
        JoyStickInput();

        if (direction.magnitude >= 0.01f)
            PlayerMovement();              
        else
            IdleAnim();             
    }

    public void PlayerMovement()
    {
        RunAnim();
        PlayerRotation(direction);
        controller.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void JoyStickInput()
    {
        horizontal = joyStick.Horizontal;
        vertical = joyStick.Vertical;

        direction = new Vector3(horizontal, 0, vertical).normalized;
    }
}
