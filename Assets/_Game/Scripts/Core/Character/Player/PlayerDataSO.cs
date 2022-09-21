using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "PlayerData")]

public class PlayerDataSO : ScriptableObject
{
    public bool InitData;

    public string Name;

    public int Weapon;

    public int Color;
    public int Hat;
    public int Pant;

    public int Gold;

    public int Rank;
}
