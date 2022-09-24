using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    public DataManager dataIns;

    public Firing firing;

    public CanvasInfoBar canvasInfoBar;

    public CharacterController controller;

    public GameObject characterObject, weaponHolder, hatHolder, canvasInfoObject;

    public SkinnedMeshRenderer pantHolder, bodyMeshRenderer;

    public Color32 bodyColor;

    public Transform characterTransform;
    Transform currentWeaponTransform, currentHatTransform;

    public Animator animator;

    public Collider[] colliders;

    public Vector3 direction, characterOrigin, sizeUpOffset;

    public LayerMask targetLayer;

    public int characterPoint, characterLevel, characterLevelLimit, characterWeapon;

    public float playerSpeed, attackRange, deadAnimTime, deadAnimEnd, horizontal, vertical;
    float turnTime, turnVelocity;

    private string curAnimName;

    public bool isDead;

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(characterOrigin, attackRange);
    }

#endif

    public void OnTriggerEnter(Collider other)
    {
        OnGetHit();
    }

    public virtual void OnInit()
    {
        turnTime = 0.1f;
        playerSpeed = 5;
        attackRange = 6;
        deadAnimTime = 2;
        characterLevel = 1;
        characterLevelLimit = 2;
        
        characterObject = gameObject;

        sizeUpOffset = new Vector3(0.1f, 0.1f, 0.1f);
        
        dataIns = DataManager.Ins;
    }

    public void Movement(CharacterController controller)
    {
        RunAnim();

        PlayerRotation(direction);

        controller.Move(direction * playerSpeed * Time.deltaTime);

        KeepOnGround();
    }

    public virtual void OnGetHit()
    {
        isDead = true;

        deadAnimEnd = Time.time + deadAnimTime;

        controller.enabled = false;

        ChangeAnim(GameConstant.DEAD_ANIM);

        canvasInfoObject.SetActive(false);
    }

    public virtual void OnDead()
    {
        if(isDead && Time.time > deadAnimEnd)
        {
            ObjectPooling.Ins.Despawn(GameConstant.ENEMY_POOLING, gameObject);

            LevelManager.Ins.OnCharacterDead();
        }
    }

    //Rotation for movement
    public void PlayerRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        
        characterTransform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void GetCharacterPosition()
    {
        characterOrigin = characterTransform.position;
    }

    public void PlayerCircleCast()
    {
        GetCharacterPosition();

        colliders = Physics.OverlapSphere(characterOrigin, attackRange, targetLayer);
    }

    //Shoot if match condittion
    public void SetTarget()
    {
        if (InRangeCondition() && StopMovingCondition())
        {
            firing.isFiring = true;

            PlayerRotation(GetClosestEnemyCollider(colliders) - characterOrigin);
        }    
        else
        {
            firing.isFiring = false;
        }
    }

    //Find nearest Collider
    public Vector3 GetClosestEnemyCollider(Collider[] enemyColliders)
    {
        float bestDistance = 10000;
        Collider bestCollider = null;

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            if (enemyColliders[i] != null)
            {
                float distance = Vector3.Distance(characterOrigin, enemyColliders[i].transform.position);

                if (distance < bestDistance && distance >= 0.01f)
                {
                    bestDistance = distance;
                    bestCollider = enemyColliders[i];
                }
            }
        }

        return bestCollider.transform.position;
    }

    public virtual void OnGetKill(Character character)
    {
        this.characterPoint += character.characterLevel;
        canvasInfoBar.pointText.text = this.characterPoint.ToString();

        if(characterPoint >= characterLevelLimit)
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        characterLevel++;
        
        characterLevelLimit = characterLevelLimit * 2 + 1;
        
        GainStat();
    }

    public virtual void GainStat()
    {
        characterTransform.localScale += sizeUpOffset;

        attackRange += 0.6f;
    }

    public bool InRangeCondition()
    {
        if (Physics.CheckSphere(characterOrigin, attackRange, targetLayer) && colliders.Length >= 2)
        {
            return true;
        }

        return false;
    }

    public bool StopMovingCondition()
    {
        if (direction.magnitude < 0.01f)
        {
            return true;
        }

        return false;
    }

    public void KeepOnGround()
    {
        characterTransform.localPosition = new Vector3(characterTransform.localPosition.x, 0, characterTransform.localPosition.z);
    }


    public void GetWeapon(int weaponIndex)
    {
        if (currentWeaponTransform != null)
        {
            Destroy(currentWeaponTransform.gameObject);
        }

        SetItemTransform(dataIns.weaponObjectList[weaponIndex], ref currentWeaponTransform, weaponHolder.transform, Vector3.zero, Quaternion.Euler(0, 0, -100));

        characterWeapon = weaponIndex;
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

    public void GetBodyColor(Color32 color)
    {
        bodyMeshRenderer.material.color = color;
        bodyColor = color;
    }

    public void SetItemTransform(GameObject item, ref Transform itemTransform, Transform parentTransform, Vector3 position, Quaternion rotation)
    {
        itemTransform = Instantiate(item).transform;
        itemTransform.SetParent(parentTransform);
        itemTransform.localPosition = position;
        itemTransform.localRotation = rotation;
    }

    #region ANIMATORREGION

    public void RunAnim()
    {
        animator.ResetTrigger(GameConstant.IDLE_ANIM);
        animator.ResetTrigger(GameConstant.ATTACK_ANIM);
    }

    public void ChangeAnim(string animName)
    {
        if (curAnimName != null)
        {
            animator.ResetTrigger(curAnimName);
        }

        curAnimName = animName;
        animator.SetTrigger(curAnimName);
    }
    #endregion
}
