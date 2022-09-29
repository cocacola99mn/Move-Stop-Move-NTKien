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
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
        else
        {
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }


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
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        LevelManager.Ins.LevelStarter(true);
        Destroy(gameObject);
    }

    public void SoundOnButton()
    {
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
        AudioManager.Ins.SetAudio(false);
    }

    public void SoundOffButton()
    {
        SoundOn.SetActive(true);
        SoundOff.SetActive(false);
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        AudioManager.Ins.SetAudio(true);
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
