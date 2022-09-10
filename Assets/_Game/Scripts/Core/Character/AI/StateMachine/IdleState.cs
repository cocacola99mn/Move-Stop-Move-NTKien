using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<AIController>
{
    public void OnEnter(AIController ai)
    {
        ai.direction = new Vector3(0, 0, 0).normalized;

        ai.AIRandomTimer(2 , 3);
    }

    public void OnExecute(AIController ai)
    {
        ai.IdleAnim();

        if (ai.InRangeCondition())
            ai.ChangeState(new AttackState());
            
        ai.AIRandomStateTime(new PatrolState());        
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
