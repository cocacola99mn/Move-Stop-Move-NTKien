using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    DataManager dataIns;
    public Text zoneText, rankText, coinText, nameFieldText;
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
        nameFieldText.text = dataIns.playerDataSO.Name;

        if (PlayerPrefs.GetInt(GameConstant.PREF_SOUND) == 1)
            SoundOffButton();
        else
            SoundOnButton();

        if (PlayerPrefs.GetInt(GameConstant.PREF_VIBRATE) == 1)
            VibrateOffButton();
        else
            VibrateOnButton();
    }

    public void PlayGameButton()
    {
        CameraController.Ins.MoveToPlayer();
        LevelManager.Ins.LevelStarter(true);

        if(string.Equals(nameFieldText.text, ""))
        {
            dataIns.SetStringData(GameConstant.DEFAULT_NAME, ref dataIns.playerDataSO.Name, GameConstant.DEFAULT_NAME);
        }
        else
        {
            dataIns.SetStringData(GameConstant.PREF_PLAYERNAME, ref dataIns.playerDataSO.Name, nameFieldText.text);
        }

        dataIns.player.characterNameText.text = dataIns.playerDataSO.Name;

        Destroy(gameObject);
    }

    public void WeaponShopButton()
    {
        UIManager.Ins.OpenUI(UIID.UICWeaponShop);
        Destroy(gameObject);
    }

    public void SkinShopButton()
    {
        UIManager.Ins.OpenUI(UIID.UICSkinShop);
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
        PlayerPrefs.SetInt(GameConstant.PREF_SOUND, 0);
    }

    public void SoundOffButton()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        PlayerPrefs.SetInt(GameConstant.PREF_SOUND, 1);
    }

    public void AdsButton()
    {
        Debug.Log("Turn off Ads");
    }
}
