using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkin", menuName = "PantData")]
public class PantData : ScriptableObject
{
    public Sprite pantSprite;

    public GameObject pant;

    public Material pantMaterial;

    public int price, skinId;

    public string skinDes;

    public bool locked;
}
