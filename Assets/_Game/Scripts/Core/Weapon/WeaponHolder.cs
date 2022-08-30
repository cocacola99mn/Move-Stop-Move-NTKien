using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public List<Weapon> weapons;

    public List<GameObject> playerWeapons;

    public Transform holder;

    public GameObject myWeapon;

    public int currentWeapon;

    public void Start()
    {
        currentWeapon = PlayerPrefs.GetInt(GameConstant.PREF_WEAPONEQUIP);
    }

    public void GetWeapon()
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            myWeapon = Instantiate(weapons[i].weapon);

            if (i == currentWeapon)
                myWeapon.SetActive(true);
            else
                myWeapon.SetActive(false);

            myWeapon.transform.SetParent(holder);
            myWeapon.transform.localPosition = new Vector3(0, 0, 0);

            playerWeapons.Add(myWeapon);
        }
    }
}
