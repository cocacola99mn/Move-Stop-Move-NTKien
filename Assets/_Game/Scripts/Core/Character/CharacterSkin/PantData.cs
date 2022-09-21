using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "PantData")]
public class PantData : ScriptableObject
{
    public GameObject pant;

    public Material pantMaterial;

    public int price, skinId;

    public string skinDes;

    public bool locked;
}
