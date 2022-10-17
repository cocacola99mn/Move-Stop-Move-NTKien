using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    DataManager dataIns;
    public Text zoneText, rankText, coinText, nameFieldText;
    public InputField nameInput;
    public GameObject vibrateOn, soundOn, vibrateOff, soundOff;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;
        coinText.text = dataIns.playerDataSO.Gold.ToString();
        zoneText.text = "ZONE " + dataIns.playerDataSO.Zone.ToString();
        rankText.text = "BEST " + dataIns.playerDataSO.Rank.ToString();
        nameInput.text = dataIns.playerDataSO.Name;
        if (PlayerPrefs.GetInt(GameConstant.PREF_SOUND) == 1)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
        else
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
        }

        if (PlayerPrefs.GetInt(GameConstant.PREF_VIBRATE) == 1)
        {
            VibrateOffButton();
        }
        else
        {
            VibrateOnButton();
        }

        GameManager.Ins.cameraScaler.matchWidthOrHeight = 0.55f;
    }

    public void PlayGameButton()
    {
        CameraController.Ins.MoveToPlayer();
        LevelManager.Ins.SetGameplayUI(true);
        LevelManager.Ins.levelStarter = true;

        if(string.Equals(nameInput.text, ""))
        {
            dataIns.SetStringData(GameConstant.DEFAULT_NAME, ref dataIns.playerDataSO.Name, GameConstant.DEFAULT_NAME);
        }
        else
        {
            dataIns.SetStringData(GameConstant.PREF_PLAYERNAME, ref dataIns.playerDataSO.Name, nameInput.text);
        }

        dataIns.player.characterNameText.text = dataIns.playerDataSO.Name;
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        Destroy(gameObject);
    }

    public void WeaponShopButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        UIManager.Ins.OpenUI(UIID.UICWeaponShop);
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 1;
        Destroy(gameObject);
    }

    public void SkinShopButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        UIManager.Ins.OpenUI(UIID.UICSkinShop);
        GameManager.Ins.cameraScaler.matchWidthOrHeight = 1;
        Destroy(gameObject);
    }

    public void VibrateOnButton()
    {
        vibrateOff.SetActive(true);
        vibrateOn.SetActive(false);
        PlayerPrefs.SetInt(GameConstant.PREF_VIBRATE, 0);
    }

    public void VibrateOffButton()
    {
        vibrateOff.SetActive(false);
        vibrateOn.SetActive(true);
        PlayerPrefs.SetInt(GameConstant.PREF_VIBRATE, 1);
    }

    public void SoundOnButton()
    {
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        AudioManager.Ins.SetAudio(false);
    }

    public void SoundOffButton()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        AudioManager.Ins.SetAudio(true);
    }

    public void AdsButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        Debug.Log("Turn off Ads");
    }
}
