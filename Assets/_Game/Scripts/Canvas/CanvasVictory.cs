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
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 1;
        coinText.text = LevelManager.Ins.playerController.characterPoint.ToString();
        AudioManager.Ins.PlayAudio(AudioName.Victory);
    }

    public void NextZoneButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 0.55f;
        GameManager.Ins.NextScene();
    }
}
