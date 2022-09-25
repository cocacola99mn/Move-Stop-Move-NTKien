using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public void PauseButton()
    {
        UIManager.Ins.OpenUI(UIID.UICPause);
        LevelManager.Ins.LevelStarter(false);
    }
}
