using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject UIGameplay,indicatorHolder;

    public EnemySpawner enemySpawner;

    public Text UIAliveDisplayNumber;
    
    public int aliveNumber;

    public bool levelStarter;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        aliveNumber = 100;
        SetAliveNumber();
    }

    public void SetAliveNumber()
    {
        UIAliveDisplayNumber.text = aliveNumber.ToString();
    }

    public void LevelStarter(bool state)
    {
        levelStarter = state;
        indicatorHolder.SetActive(state);
        UIGameplay.SetActive(state);
    }

    public void OnCharacterDead()
    {
        aliveNumber--;

        SetAliveNumber();

        if(aliveNumber > 6)
        {
            enemySpawner.StartCoroutine(enemySpawner.SpawnEnemyOnDead());
        }
    }

    public void OnLevelFail()
    {
        UIManager.Ins.OpenUI(UIID.UICFail);
        LevelStarter(false);
    }

    public void OnLevelVictory()
    {

    }
}
