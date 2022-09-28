using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    public Text coinText;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        coinText.text = LevelManager.Ins.playerController.characterPoint.ToString();
    }

    public void NextZoneButton()
    {

        GameManager.Ins.NextScene();
    }
}
