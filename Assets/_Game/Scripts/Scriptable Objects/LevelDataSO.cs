using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "LevelData")]

public class LevelDataSO : ScriptableObject
{
    public int Zone;
    public int AliveNum;
}
