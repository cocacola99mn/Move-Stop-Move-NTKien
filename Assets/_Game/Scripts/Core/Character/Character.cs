using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Firing firing;
    public Transform character;
    public Animator animator;

    public float turnTime, turnVelocity, playerSpeed, attackRange;
    public float horizontal, vertical;

    public Collider[] colliders;

    public Vector3 direction, characterOrigin;
    
    public LayerMask targetLayer;

    public void OnInit()
    {
        if (GetComponent<Firing>() != null)
            firing = GetComponent<Firing>();

        turnTime = 0.1f;
        playerSpeed = 8;
        attackRange = 5;
    }

    public void PlayerRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        
        character.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void GetCharacterPosition()
    {
        characterOrigin = character.position;
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
            firing.isFiring = false;
    }

    public Vector3 GetClosestEnemyCollider(Collider[] enemyColliders)
    {
        float bestDistance = 10000;
        Collider bestCollider = null;

        foreach (Collider enemy in enemyColliders)
        {
            if(enemy != null)
            {
                float distance = Vector3.Distance(characterOrigin, enemy.transform.position);

                if (distance < bestDistance && distance >= 0.01f)
                {
                    bestDistance = distance;
                    bestCollider = enemy;
                }
            }                 
        }

        return bestCollider.transform.position;

    }

    public bool InRangeCondition()
    {
        if (Physics.CheckSphere(characterOrigin, attackRange, targetLayer) && colliders.Length >= 2)
            return true;
        else
            return false;
    }

    public bool StopMovingCondition()
    {
        if (direction.magnitude < 0.01f)
            return true;
        else
            return false;
    }

    #region ANIMATORREGION

    public void WinAnim()
    {
        animator.SetTrigger(GameConstant.WIN_ANIM);
    }

    public void DeadAnim()
    {
        animator.SetTrigger(GameConstant.DEAD_ANIM);
    }

    public void IdleAnim()
    {
        animator.SetTrigger(GameConstant.IDLE_ANIM);
        animator.ResetTrigger(GameConstant.ATTACK_ANIM);
    }

    public void AttackAnim()
    {
        animator.SetTrigger(GameConstant.ATTACK_ANIM);
    }

    public void RunAnim()
    {
        animator.ResetTrigger(GameConstant.IDLE_ANIM);
        animator.ResetTrigger(GameConstant.ATTACK_ANIM);
    }

    #endregion
}
