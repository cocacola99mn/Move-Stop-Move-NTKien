using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Character
{
    public IState<AIController> currentState;

    public CharacterController AIControl;

    public float timer, secondsFloatTimer, randomTimer;

    public AIRandomWeapon aIRandomWeapon;

    public int randomWeaponIndex;
    void Start()
    {
        OnInit();

        randomWeaponIndex = aIRandomWeapon.weaponIndex;

        ChangeState(new IdleState());
    }

    void Update()
    {
        StartAi();
    }

    public void StartAi()
    {
        if (LevelManager.Ins.levelStarter)
        {
            Timer();
            ExecuteState();
            PlayerCircleCast();
            SetTarget();
        }
    }

    public void AIMovement()
    {
        RunAnim();
        
        PlayerRotation(direction);

        AIControl.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void AIRandomStateTime(IState<AIController> state)
    {
        if (secondsFloatTimer >= randomTimer)
            ChangeState(state);  
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        secondsFloatTimer = (float)(timer % 60);
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }

    public void AIRandomTimer(float min, float max)
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
