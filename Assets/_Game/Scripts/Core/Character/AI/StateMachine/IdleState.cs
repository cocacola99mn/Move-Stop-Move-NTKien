using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<AIController>
{
    public void OnEnter(AIController ai)
    {
        ai.direction = Vector3.zero.normalized;

        ai.RandomTimer(2 , 3);
    }

    public void OnExecute(AIController ai)
    {
        ai.ChangeAnim(GameConstant.IDLE_ANIM);

        if (ai.InRangeCondition())
            ai.ChangeState(ai.attackState);
            
        ai.RandomStateTime(ai.patrolState);        
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
