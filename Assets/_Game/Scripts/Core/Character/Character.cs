using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    public CharacterController controller;

    public GameObject characterObject;

    public Firing firing;

    public Transform characterTransform;

    public Animator animator;

    public Collider[] colliders;

    public Vector3 direction, characterOrigin;

    public LayerMask targetLayer;

    public int characterPoint, playerWeapon;

    public float turnTime, turnVelocity, playerSpeed, attackRange, deadAnimTime, deadAnimEnd;
    public float horizontal, vertical;

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
        characterObject = gameObject;
    }

    public void Movement(CharacterController controller)
    {
        RunAnim();

        PlayerRotation(direction);

        controller.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void OnGetHit()
    {
        isDead = true;

        deadAnimEnd = Time.time + deadAnimTime;

        controller.enabled = false;

        ChangeAnim(GameConstant.DEAD_ANIM);
    }

    public virtual void OnDead()
    {
        if(isDead && Time.time > deadAnimEnd)
        {
            ObjectPooling.Ins.Despawn(GameConstant.ENEMY_POOLING, gameObject);

            LevelManager.Ins.OnCharacterDead();
        }
    }

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

    public void SetCharacterScale(float x)
    {
        characterTransform.localScale += new Vector3(x, x, x);
    }

    public void OnGetKill(Character character)
    {
        Debug.Log("Got Kill");
    }

    public bool InRangeCondition()
    {
        if (Physics.CheckSphere(characterOrigin, attackRange, targetLayer) && colliders.Length >= 2)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool StopMovingCondition()
    {
        if (direction.magnitude < 0.01f)
        {
            return true;
        }

        else
        {
            return false;
        }
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
