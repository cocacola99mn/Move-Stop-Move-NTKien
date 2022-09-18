using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public PlayerController playerController;
    public WeaponHolder weaponHolder;
    int currentWeapon;

    public void OnChangeWeapon()
    {
        weaponHolder.playerWeapons[currentWeapon].SetActive(false);
    }

    public void EquipWeapon()
    {
        GetWeaponPref();

        weaponHolder.playerWeapons[currentWeapon].SetActive(true);

        playerController.playerWeapon = currentWeapon;
    }

    public int GetWeaponPref()
    {
        currentWeapon = PlayerPrefs.GetInt(GameConstant.PREF_WEAPONEQUIP);

        return PlayerPrefs.GetInt(GameConstant.PREF_WEAPONEQUIP);
    }

    public void SetWeaponPref(int id)
    {
        PlayerPrefs.SetInt(GameConstant.PREF_WEAPONEQUIP, id);
    }
}
