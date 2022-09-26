using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasPause : UICanvas
{
    public GameObject SoundOn, SoundOff, VibrateOn, VibrateOff;

    public void HomeButton()
    {
        GameManager.Ins.Restart();
    }

    public void ContinueButton()
    {
        Close();
        LevelManager.Ins.LevelStarter(true);
    }

    public void SoundOnButton()
    {
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
    }

    public void SoundOffButton()
    {
        SoundOn.SetActive(true);
        SoundOff.SetActive(false);
    }

    public void VibrateOnButton()
    {
        VibrateOn.SetActive(false);
        VibrateOff.SetActive(true);
    }

    public void VibrateOffButton()
    {
        VibrateOn.SetActive(true);
        VibrateOff.SetActive(false);
    }
}
