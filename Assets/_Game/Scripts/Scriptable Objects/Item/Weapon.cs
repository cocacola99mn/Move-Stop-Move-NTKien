using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int price, id;

    public GameObject weapon;

    public string weaponName, description;

    public bool locked;
}
