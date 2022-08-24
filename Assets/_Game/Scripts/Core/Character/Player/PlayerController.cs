using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public CharacterController controller;
    public Transform TargetOutline;

    public Joystick joyStick;

    float horizontal, vertical;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        PlayerCircleCast();
        DisplayTarget();
        PlayerAction();
    }

    public void PlayerAction()
    {
        JoyStickInput();

        if (!StopMovinglCondition())
            PlayerMovement();                 
        else if(InRangeCondition() && StopMovinglCondition() && firing.shotCounter <= 0.8f)
            AttackAnim();       
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

    public void DisplayTarget()
    {
        if(InRangeCondition())
        {
            Vector3 enemyPos = GetClosestEnemyCollider(colliders);
            enemyPos.y += 0.01f;

            TargetOutline.gameObject.SetActive(true);
            TargetOutline.position = enemyPos;
        }
        else
            TargetOutline.gameObject.SetActive(false);
    }
}