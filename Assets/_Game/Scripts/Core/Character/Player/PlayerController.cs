using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{   
    public Character killer;
    public Joystick joyStick;

    public ParticleSystem levelUpParticle;
    public Transform TargetOutline, rangeOutline, particleTransform;

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

    private void LateUpdate()
    {
        particleTransform = characterTransform;
    }

    public override void OnInit()
    {
        base.OnInit();

        rangeOutline.localScale = new Vector3(attackRange * 0.8f, attackRange * 0.8f, 1);
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

    public void Action()
    {
        JoyStickInput();

        if (!StopMovingCondition())
        {
            Movement(controller);
        }
        else if (InRangeCondition() && StopMovingCondition())
        {
            ChangeAnim(GameConstant.ATTACK_ANIM);
        }
        else
        {
            ChangeAnim(GameConstant.IDLE_ANIM);
        }
    }

    public override void OnGetHit(Collider other)
    {
        base.OnGetHit(other);
        killer = Cache.GetProjectileController(other.gameObject).bulletShooter;
    }

    public override void OnDead()
    {
        if (isDead && Time.time > deadAnimEnd)
        {
            gameObject.SetActive(false);
            if (!LevelManager.Ins.reviveCheck)
            {
                UIManager.Ins.OpenUI(UIID.UICRevive);
            }
            else
            {
                UIManager.Ins.OpenUI(UIID.UICFail);
            }
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

    public override void OnWeaponBoost(float range, bool value)
    {
        base.OnWeaponBoost(range, value);
        SetRangeOutline(range * 0.8f);
    }

    public void SetRangeOutline(float x)
    {
        rangeOutline.localScale += new Vector3(x, x, 1);
    }

    public override void GainStat()
    {
        base.GainStat();
        PlayAudioIfInScreen(AudioName.SizeUp);
        ParticlePool.Play(levelUpParticle, characterOrigin, Quaternion.identity);
        CameraController.Ins.MoveFurtherFromPlayer();
    }
}
