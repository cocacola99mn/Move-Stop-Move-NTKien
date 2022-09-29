using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Spawner spawner;
    public PlayerController playerController;

    public GameObject UIGameplay,indicatorHolder;
    public Text UIAliveDisplayNumber;
    
    public int aliveNumber, newGoldNum, zoneIndex;
    public bool levelStarter;

    void Start()
    {
        OnInit();

    }

    public void OnInit()
    {
        zoneIndex = DataManager.Ins.playerDataSO.Zone - 1;
        aliveNumber = DataManager.Ins.levelDataSOList[zoneIndex].AliveNum;
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
        playerController.controller.enabled = state;
    }

    public void OnCharacterDead()
    {
        aliveNumber--;
        SetAliveNumber();

        if (aliveNumber > 6)
        {
            spawner.StartCoroutine(spawner.SpawnEnemyOnDead());
        }
        if(aliveNumber < 2 && !playerController.isDead)
        {
            OnLevelVictory();
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
        LevelStarter(false);
        DataManager.Ins.SetIntData(GameConstant.PREF_ZONE, ref DataManager.Ins.playerDataSO.Zone, DataManager.Ins.playerDataSO.Zone + 1);
        UIManager.Ins.OpenUI(UIID.UICVictory);
        playerController.ChangeAnim(GameConstant.DANCE_ANIM);
    }
}
