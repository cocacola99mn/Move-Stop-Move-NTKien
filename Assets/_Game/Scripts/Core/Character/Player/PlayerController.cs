using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public Joystick joyStick;

    public Transform TargetOutline, rangeOutline;

    Vector3 moveDirection;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        OnDead();
        StartPlayer();
    }

    public override void OnInit()
    {
        base.OnInit();

        SetRangeOutline(attackRange - 1, attackRange - 1);

        GetWeapon(dataIns.playerDataSO.Weapon);

        GetHat(dataIns.playerDataSO.Hat);

        GetPant(dataIns.playerDataSO.Pant);

        bodyColor = dataIns.colorList[2];

        canvasInfoBar.SetColor(bodyColor);
    }    

    public void StartPlayer()
    {
        if (LevelManager.Ins.levelStarter && !isDead)
        {
            PlayerCircleCast();
            SetTarget();
            DisplayTarget();
            Action();
            canvasInfoObject.SetActive(true);
        }
        else
        {
            firing.isFiring = false;
            TargetOutline.gameObject.SetActive(false);
        }
    }

    public override void OnDead()
    {
        base.OnDead();

        if (isDead && Time.time > deadAnimEnd)
        {
            LevelManager.Ins.levelStarter = false;
        }
    }

    public void Action()
    {
        JoyStickInput();

        if (!StopMovingCondition())
        {
            Movement(controller);
        }

        else if (InRangeCondition() && StopMovingCondition() && firing.shotCounter <= 0.8f)
        {
            ChangeAnim(GameConstant.ATTACK_ANIM);
        }

        else
        {
            ChangeAnim(GameConstant.IDLE_ANIM);
        }
    }

    public void JoyStickInput()
    {
        horizontal = joyStick.Horizontal;
        vertical = joyStick.Vertical;

        moveDirection.x = horizontal;
        moveDirection.y = 0;
        moveDirection.z = vertical;

        direction = moveDirection.normalized;
    }

    public void DisplayTarget()
    {
        if (InRangeCondition())
        {
            Vector3 enemyPos = GetClosestEnemyCollider(colliders);
            enemyPos.y += 0.01f;

            TargetOutline.gameObject.SetActive(true);
            TargetOutline.position = enemyPos;
        }

        else
        {
            TargetOutline.gameObject.SetActive(false);
        }
    }

    public void SetRangeOutline(float x, float y)
    {
        rangeOutline.localScale = new Vector3(x, y, 1);
    }

    public override void GainStat()
    {
        base.GainStat();
        CameraController.Ins.MoveFurtherFromPlayer();
    }
}
