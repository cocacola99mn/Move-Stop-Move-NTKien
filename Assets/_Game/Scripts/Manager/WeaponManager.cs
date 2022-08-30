using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public WeaponHolder weaponHolder;
    int currentWeapon;

    public void OnChangeWeapon()
    {
        GetWeaponPref();

        weaponHolder.playerWeapons[currentWeapon].SetActive(false);
    }

    public void EquipWeapon()
    {
        GetWeaponPref();

        weaponHolder.playerWeapons[currentWeapon].SetActive(true);
    }

    public void GetWeaponPref()
    {
        currentWeapon = PlayerPrefs.GetInt(GameConstant.PREF_WEAPONEQUIP);
    }
}
