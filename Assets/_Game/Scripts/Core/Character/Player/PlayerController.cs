using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public WeaponHolder weaponHolder;

    public CharacterController controller;
    
    public Transform TargetOutline, rangeOutline;

    public Joystick joyStick;

    public int playerWeapon;

    void Start()
    {
        OnInit();

        rangeOutline.localScale = new Vector3(attackRange - 1, attackRange - 1, 1);

        SetWeapon();

        playerWeapon = WeaponManager.Ins.GetWeaponPref();
    }

    void Update()
    {
        StartPlayer();
    }

    public void StartPlayer()
    {
        if (LevelManager.Ins.levelStarter)
        {
            PlayerCircleCast();
            SetTarget();
            DisplayTarget();
            PlayerAction();
        }
    }

    public void PlayerAction()
    {
        JoyStickInput();

        if (!StopMovingCondition())
            PlayerMovement();                 
        else if(InRangeCondition() && StopMovingCondition() && firing.shotCounter <= 0.85f)
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

    public void SetWeapon()
    {
        weaponHolder.GetWeapon();
    }
}
