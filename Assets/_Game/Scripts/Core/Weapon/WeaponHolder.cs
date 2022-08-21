using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public List<Weapon> weapons;

    public Transform holder;

    private void Start()
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            GameObject myWeapon = Instantiate(weapons[i].weapon);
            
            if (i == 0)
                myWeapon.SetActive(true); 
            else
                myWeapon.SetActive(false);

            myWeapon.transform.SetParent(holder);
            myWeapon.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
