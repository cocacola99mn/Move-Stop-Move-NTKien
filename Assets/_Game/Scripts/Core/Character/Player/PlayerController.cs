using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public Joystick joyStick;

    public GameObject weaponHolder, hatHolder;

    public SkinnedMeshRenderer pantHolder;

    public Transform TargetOutline, rangeOutline;
    Transform currentWeaponTransform, currentHatTransform;

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

    public void GetWeapon(int weaponIndex)
    {
        if(currentWeaponTransform != null)
        {
            Destroy(currentWeaponTransform.gameObject);
        }

        SetItemTransform(dataIns.weaponObjectList[weaponIndex], ref currentWeaponTransform, weaponHolder.transform, Vector3.zero, Quaternion.Euler(0, 0, -100));

        playerWeapon = weaponIndex;
    }

    public void GetHat(int hatIndex)
    {
        if (currentHatTransform != null)
        {
            Destroy(currentHatTransform.gameObject);
        }

        SetItemTransform(dataIns.hatObjectList[hatIndex], ref currentHatTransform, hatHolder.transform, dataIns.hatObjectList[hatIndex].transform.localPosition, Quaternion.identity);
    }

    public void GetPant(int pantIndex)
    {
        pantHolder.material = dataIns.pantMaterialList[pantIndex];
    }

    public void SetItemTransform(GameObject item , ref Transform itemTransform , Transform parentTransform, Vector3 position, Quaternion rotation)
    {
        itemTransform = Instantiate(item).transform;
        itemTransform.SetParent(parentTransform);
        itemTransform.localPosition = position;
        itemTransform.localRotation = rotation;

        Debug.Log(position);
    }
}
