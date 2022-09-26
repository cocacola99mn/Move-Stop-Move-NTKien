using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<AIController>
{
    public void OnEnter(AIController ai)
    {
        ai.RandomTimer(1, 3);
        ai.randomTimer = 0.9f * ai.randomTimer;
    }

    public void OnExecute(AIController ai)
    {
        if (ai.InRangeCondition() && ai.StopMovingCondition())
            ai.ChangeAnim(GameConstant.ATTACK_ANIM);
        else
            ai.ChangeAnim(GameConstant.IDLE_ANIM);
        
        ai.RandomStateTime(ai.patrolState);
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
