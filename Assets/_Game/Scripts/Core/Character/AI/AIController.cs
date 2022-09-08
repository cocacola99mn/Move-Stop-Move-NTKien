using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Character
{
    public IState<AIController> currentState;

    public CharacterController AIControl;

    public int randomTimer,secondsTimer;

    public float timer;

    void Start()
    {
        OnInit();
        ChangeState(new IdleState());
    }

    void Update()
    {
        Timer();
        ExecuteState();
    }

    public void AIMovement()
    {
        RunAnim();
        
        PlayerRotation(direction);

        AIControl.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void AIRandomStopTime()
    {
        if (secondsTimer >= randomTimer)
            ChangeState(new PatrolState());  
    }

    public void AIRandomPatrolTime()
    {
        if (secondsTimer >= randomTimer)
            ChangeState(new IdleState());
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        secondsTimer = (int)(timer % 60);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }

    public void AIRandomTimer(int min, int max)
    {
        randomTimer = Random.Range(min, max);
    }

    public void AIRandomDirection()
    {
        horizontal = Random.Range(-1.0f, 1.0f);
        vertical = Random.Range(-1.0f, 1.0f);
    }

    #region STATEMACHINE

    public void ExecuteState()
    {
        if (currentState != null)
            currentState.OnExecute(this);
    }

    public void ChangeState(IState<AIController> state)
    {
        if (currentState != null)
            currentState.OnExit(this);

        currentState = state;

        if (currentState != null)
            currentState.OnEnter(this);
    }

    public bool isState(IState<AIController> state)
    {
        if (state == currentState)
            return true;
        else 
            return false;
    }

    #endregion
}
