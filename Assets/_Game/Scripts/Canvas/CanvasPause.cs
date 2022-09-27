using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasPause : UICanvas
{
    public GameObject SoundOn, SoundOff, VibrateOn, VibrateOff;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        if (PlayerPrefs.GetInt(GameConstant.PREF_SOUND) == 1)
            SoundOffButton();
        else
            SoundOnButton();

        if (PlayerPrefs.GetInt(GameConstant.PREF_VIBRATE) == 1)
            VibrateOffButton();
        else
            VibrateOnButton();
    }

    public void HomeButton()
    {
        GameManager.Ins.Restart();
    }

    public void ContinueButton()
    {
        LevelManager.Ins.LevelStarter(true);
        Destroy(gameObject);
    }

    public void SoundOnButton()
    {
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
        PlayerPrefs.SetInt(GameConstant.PREF_SOUND, 0);
    }

    public void SoundOffButton()
    {
        SoundOn.SetActive(true);
        SoundOff.SetActive(false);
        PlayerPrefs.SetInt(GameConstant.PREF_SOUND, 1);
    }

    public void VibrateOnButton()
    {
        VibrateOn.SetActive(false);
        VibrateOff.SetActive(true);
        PlayerPrefs.SetInt(GameConstant.PREF_VIBRATE, 0);
    }

    public void VibrateOffButton()
    {
        VibrateOn.SetActive(true);
        VibrateOff.SetActive(false);
        PlayerPrefs.SetInt(GameConstant.PREF_VIBRATE, 1);
    }
}
