using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "HatData")]
public class HatData : ScriptableObject
{
    public GameObject hat;

    public int price, skinId;

    public string skinDes;

    public bool locked;
}