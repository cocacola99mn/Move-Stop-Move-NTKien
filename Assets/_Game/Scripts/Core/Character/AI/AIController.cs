using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Character
{
    public IState<AIController> currentState;

    public float timer, secondsFloatTimer, randomTimer;
    public IdleState idleState { get; protected set; }
    public PatrolState patrolState { get; protected set; }
    public AttackState attackState { get; protected set; }

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        OnDead();
        StartAi();
    }

    public override void OnInit()
    {
        base.OnInit();
        idleState = new IdleState();
        patrolState = new PatrolState();
        attackState = new AttackState();

        characterNameText.text = PickRandomName();
        characterNameString = characterNameText.text;

        ChangeState(idleState);

        GetWeapon(Random.Range(0, dataIns.weaponObjectList.Count - 1));
        GetPant(Random.Range(0, dataIns.pantMaterialList.Count - 1));
        GetHat(Random.Range(0, dataIns.hatObjectList.Count - 1));
        GetBodyColor(dataIns.colorList[Random.Range(0, dataIns.colorList.Count - 1)]);

        canvasInfoBar.SetColor(bodyColor);
        
        if(indicator != null)
        {
            indicator.SetIndicatorColor(bodyColor);
            indicator.SetIndicatorPoint(characterPoint);
        }
    }

    public void StartAi()
    {
        if (LevelManager.Ins.levelStarter && isDead == false)
        {
            Timer();
            ExecuteState();
            PlayerCircleCast();
            SetTarget();
            canvasInfoObject.SetActive(true);
        }
        else
        {
            firing.isFiring = false;
        }            
    }


    public string PickRandomName()
    {
        int randomName = Random.Range(0, GameConstant.names.Length - 1);
        return GameConstant.names[randomName];
    }

    public void RandomStateTime(IState<AIController> state)
    {
        if (secondsFloatTimer >= randomTimer)
        {
            ChangeState(state);
        }
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

    public void RandomTimer(float min, float max)
    {
        randomTimer = Random.Range(min, max);
    }

    public void RandomDirection()
    {
        horizontal = Random.Range(-1.0f, 1.0f);
        vertical = Random.Range(-1.0f, 1.0f);
    }

    public override void OnGetKill(Character character)
    {
        base.OnGetKill(character);
        indicator.SetIndicatorPoint(characterPoint);
    }

    public override void OnGetHit(Collider other)
    {
        base.OnGetHit(other);
        indicator.DespawnIndicator();
    }

    public override void OnRevive()
    {
        base.OnRevive();
        indicator.SetIndicatorColor(bodyColor);
        indicator.SetIndicatorPoint(characterPoint);
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

    public bool IsState(IState<AIController> state)
    {
        if (state == currentState)
            return true;
        else 
            return false;
    }

    #endregion
}
