using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayGameButton()
    {
        CameraController.Ins.MoveToPlayer();
        Close();
        LevelManager.Ins.UIGameplay.SetActive(true);
        LevelManager.Ins.levelStarter = true;
    }

    public void WeaponShopButton()
    {
        UIManager.Ins.OpenUI(UIID.UICWeaponShop);
        Close();
    }

    public void SkinShopButton()
    {
        UIManager.Ins.OpenUI(UIID.UICSkinShop);
        Close();
    }

    public void VibrateButton()
    {

    }

    public void SoundButton()
    {

    }
}
