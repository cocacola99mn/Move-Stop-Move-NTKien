using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayGameButton()
    {
        CameraController.Ins.MoveToPlayer();
        Close();
        UIManager.Ins.OpenUI(UIID.UICGameplay);
    }

    public void WeaponShopButton()
    {
        UIManager.Ins.OpenUI(UIID.UICWeaponShop);
        Close();
    }

    public void SkinShopButton()
    {

    }

    public void VibrateButton()
    {

    }

    public void SoundButton()
    {

    }
}
