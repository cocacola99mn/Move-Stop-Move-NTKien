using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public EnemySpawner enemySpawner;
    public PlayerController playerController;

    public GameObject UIGameplay,indicatorHolder;
    public Text UIAliveDisplayNumber;
    
    public int aliveNumber, newGoldNum;
    public bool levelStarter;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        aliveNumber = 50;
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

        //Gold for player
        newGoldNum = DataManager.Ins.playerDataSO.Gold + playerController.characterPoint;
        DataManager.Ins.SetIntData(GameConstant.PREF_GOLD, ref DataManager.Ins.playerDataSO.Gold, newGoldNum);

        //Player Rank
        if(DataManager.Ins.playerDataSO.Rank > aliveNumber)
        {
            DataManager.Ins.SetIntData(GameConstant.PREF_RANK, ref DataManager.Ins.playerDataSO.Rank, aliveNumber);
        }
    }

    public void OnLevelVictory()
    {

    }
}
