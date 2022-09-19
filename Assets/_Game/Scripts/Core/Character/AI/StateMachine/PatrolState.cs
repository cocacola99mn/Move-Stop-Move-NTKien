using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<AIController>
{

    public void OnEnter(AIController ai)
    {
        ai.RandomDirection();

        ai.direction = new Vector3(ai.horizontal, 0, ai.vertical).normalized;

        ai.RandomTimer(1 , 2);
    }

    public void OnExecute(AIController ai)
    {
        ai.Movement(ai.controller);

        ai.RandomStateTime(ai.idleState);
    }

    public void OnExit(AIController ai)
    {
        ai.ResetTimer();
    }

}
