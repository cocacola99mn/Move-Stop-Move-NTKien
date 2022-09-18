using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public WeaponHolder weaponHolder;

    public CharacterController controller;
    
    public Transform TargetOutline, rangeOutline;

    public Joystick joyStick;

    public Character character;

    void Start()
    {
        OnInit();

        SetRangeOutline(attackRange - 1, attackRange - 1);

        SetWeapon();

        playerWeapon = WeaponManager.Ins.GetWeaponPref();
    }

    void Update()
    {
        OnDead();
        StartPlayer();
    }

    public override void OnDead()
    {
        base.OnDead();
        if (isDead && Time.time > deadAnimEnd)
            LevelManager.Ins.levelStarter = false;
    }

    public void StartPlayer()
    {
        if (LevelManager.Ins.levelStarter && !isDead)
        {
            PlayerCircleCast();
            SetTarget();
            DisplayTarget();
            PlayerAction();
        }
        else
        {
            firing.isFiring = false;
            TargetOutline.gameObject.SetActive(false);
        }
    }

    public void PlayerAction()
    {
        JoyStickInput();

        if (!StopMovingCondition())
            PlayerMovement();                 
        else if(InRangeCondition() && StopMovingCondition() && firing.shotCounter <= 0.8f)
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

    public void SetRangeOutline(float x, float y)
    {
        rangeOutline.localScale = new Vector3(x, y, 1);
    }

    public void SetWeapon()
    {
        weaponHolder.GetWeapon();
    }
}
