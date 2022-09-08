using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<AIController>
{

    public void OnEnter(AIController ai)
    {
        ai.AIRandomDirection();
        ai.direction = new Vector3(ai.horizontal, 0, ai.vertical).normalized;

        ai.AIRandomTimer(1 , 2);
    }

    public void OnExecute(AIController ai)
    {
        ai.AIMovement();

        ai.AIRandomStateTime(new IdleState());
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
