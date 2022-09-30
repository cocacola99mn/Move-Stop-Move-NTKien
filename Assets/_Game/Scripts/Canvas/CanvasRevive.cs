using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRevive : UICanvas
{
    DataManager dataIns;
    LevelManager levelIns;

    public Text timeText;
    public Transform counterBackground;
    Vector3 animMovement;

    int newGoldNum, goldNeeded, secondsIntTimer;
    float timer;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        Timer();
        BackgroundAnim();
    }

    private void OnInit()
    {
        dataIns = DataManager.Ins;
        levelIns = LevelManager.Ins;
        timer = 6f;
        goldNeeded = 100;
        animMovement = new Vector3(0, 0, -2);
        levelIns.SetGameplayUI(false);
    }

    public void CloseButton()
    {
        Close();
        levelIns.OnLevelFail();
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void CoinButton()
    {
        if(dataIns.playerDataSO.Gold > goldNeeded)
        {
            newGoldNum = dataIns.playerDataSO.Gold - goldNeeded;
            dataIns.SetIntData(GameConstant.PREF_GOLD, ref dataIns.playerDataSO.Gold, newGoldNum);
            levelIns.RevivePlayer();
            levelIns.reviveCheck = true;
            Close();
        }

        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void AdsButton()
    {
        levelIns.RevivePlayer();
        levelIns.reviveCheck = true;
        Close();
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void Timer()
    {
        timer -= Time.deltaTime;
        secondsIntTimer = (int)(timer % 60);
        timeText.text = secondsIntTimer.ToString();
        if(timer <= -0.2f)
        {
            Close();
            levelIns.OnLevelFail();
        }
    }

    public void BackgroundAnim()
    {
        counterBackground.eulerAngles += animMovement;
    }
}
