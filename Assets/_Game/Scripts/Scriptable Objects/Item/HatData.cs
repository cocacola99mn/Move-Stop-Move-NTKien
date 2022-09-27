using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkin", menuName = "HatData")]
public class HatData : ScriptableObject
{
    public Sprite hatSprite;

    public GameObject hat;

    public int price, skinId;

    public string skinDes;

    public bool locked;
}