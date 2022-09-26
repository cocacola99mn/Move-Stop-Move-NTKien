using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ItemData")]

public class ItemListData : ScriptableObject
{
    public List<Weapon> weaponList;
    public List<Projectile> projectileList;

    public List<HatData> hatDatasList;

    public List<PantData> pantDatasList;
}
