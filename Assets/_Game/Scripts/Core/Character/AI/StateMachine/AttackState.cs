using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<AIController>
{
    public void OnEnter(AIController ai)
    {
        ai.AIRandomTimer(1, 3);
        ai.randomTimer = 0.9f * ai.randomTimer;
    }

    public void OnExecute(AIController ai)
    {
        if (ai.InRangeCondition() && ai.StopMovingCondition() && ai.firing.shotCounter <= 0.8f)
            ai.AttackAnim();
        else
            ai.IdleAnim();
        
        ai.AIRandomStateTime(new PatrolState());
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
