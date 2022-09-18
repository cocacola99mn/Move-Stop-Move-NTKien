using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewList", menuName = "WeaponList")]

public class WeaponListData : ScriptableObject
{
    public List<Weapon> weaponList;
}
