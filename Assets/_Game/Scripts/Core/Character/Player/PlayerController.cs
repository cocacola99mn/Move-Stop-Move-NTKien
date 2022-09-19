using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public WeaponHolder weaponHolder;
    
    public Transform TargetOutline, rangeOutline;

    public Joystick joyStick;

    public Character character;

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

        SetWeapon();

        playerWeapon = WeaponManager.Ins.GetWeaponPref();
    }    

    public void StartPlayer()
    {
        if (LevelManager.Ins.levelStarter && !isDead)
        {
            PlayerCircleCast();
            SetTarget();
            DisplayTarget();
            Action();
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

        direction = new Vector3(horizontal, 0, vertical).normalized;
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

    public void SetWeapon()
    {
        weaponHolder.GetWeapon();
    }

    //private Weapon curretnWeapon;

    //public void ChangeWeapon(int id)
    //{
    //    if (curretnWeapon != null)
    //    {
    //        Destroy(curretnWeapon.gameobject);
    //    }

    //    curretnWeapon = Instantiate( DataManager.ins.getweapon(ind), weaponTransform);

    //}
}
