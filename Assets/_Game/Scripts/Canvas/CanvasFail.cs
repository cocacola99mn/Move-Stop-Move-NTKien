using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    public LevelManager levelIns;
    public Text topText, killerText, goldText;

    public void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        levelIns = LevelManager.Ins;
        topText.text = "#" + levelIns.aliveNumber.ToString();
        goldText.text = levelIns.playerController.characterPoint.ToString();
        killerText.text = levelIns.playerController.killer.characterNameString;
    }

    public void ContinueButton()
    {
        GameManager.Ins.Restart();
    }
}
