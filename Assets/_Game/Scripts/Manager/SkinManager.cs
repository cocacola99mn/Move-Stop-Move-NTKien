using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    public SkinHolder skinHolder;
    public Material playerDefault;
    public int GetHatPref()
    {
        //TODO: throw to PlayerDataManager
        return PlayerPrefs.GetInt(GameConstant.PREF_HATEQUIP);
    }

    public int GetPantPref()
    {
        return PlayerPrefs.GetInt(GameConstant.PREF_PANTEQUIP);
    }

    public void TryHat(int hatIndex)
    {
        for(int i = 0; i < skinHolder.hatList.Count; i++)
        {
            skinHolder.hatList[i].SetActive(false);
        }

        skinHolder.currentHat = hatIndex;

        skinHolder.hatList[hatIndex].SetActive(true);
    }

    public void TryPant(int pantIndex)
    {
        skinHolder.currentPant = pantIndex;

        skinHolder.pantMesh.material = skinHolder.pantList[pantIndex];
    }

    public void DeactiveHat()
    {
        if (GetHatPref() != 0)
        {
            skinHolder.currentHat = GetHatPref();
            skinHolder.hatList[skinHolder.currentHat - 1].SetActive(false);
        }
    }

    public void EquipHat()
    {
        if(GetHatPref() != 0)
        {
            skinHolder.currentHat = GetHatPref();
            TryHat(skinHolder.currentHat - 1);
        }
        else if(GetHatPref() == 0)
            skinHolder.hatList[skinHolder.currentHat - 1].SetActive(false);
    }

    public void EquipPant()
    {
        if(GetPantPref() != 0)
        {
            skinHolder.currentPant = GetPantPref();
            TryPant(skinHolder.currentPant - 1);
        }
        else if(GetPantPref() == 0)
            skinHolder.pantMesh.material = playerDefault;
    }

    public void SetHatPref(int id)
    {
        PlayerPrefs.SetInt(GameConstant.PREF_HATEQUIP, id);
    }

    public void SetPantPref(int id)
    {
        PlayerPrefs.SetInt(GameConstant.PREF_PANTEQUIP, id);
    }
}
