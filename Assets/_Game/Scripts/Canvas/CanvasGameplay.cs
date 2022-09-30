using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public void PauseButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        UIManager.Ins.OpenUI(UIID.UICPause);
        LevelManager.Ins.SetGameplayUI(false);
    }
}
