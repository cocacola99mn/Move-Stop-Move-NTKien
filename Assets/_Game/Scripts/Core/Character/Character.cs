using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Firing firing;
    public Transform character;
    public Animator animator;

    public float turnTime, turnVelocity, playerSpeed, attackRange;

    public Collider[] colliders;

    protected Vector3 direction, characterOrigin;
    
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

    public void PlayerCircleCast()
    {
        characterOrigin = character.position;
        
        colliders = Physics.OverlapSphere(characterOrigin, attackRange, targetLayer);

        SetTarget();
    }

    public void SetTarget()
    {
        if (direction.magnitude < 0.01f && Physics.CheckSphere(characterOrigin, attackRange, targetLayer))
        {
            firing.isFiring = true;
            
            foreach (Collider coll in colliders)
            {
                PlayerRotation(colliders[0].transform.position - characterOrigin);
                AttackAnim();
            }
        }    
        else 
            firing.isFiring = false;
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
