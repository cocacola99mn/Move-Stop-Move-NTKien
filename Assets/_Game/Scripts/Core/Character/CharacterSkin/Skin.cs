using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinType { Hat, Pant }

[CreateAssetMenu(fileName = "NewSkin", menuName = "Skin")]
public class Skin : ScriptableObject
{
    public SkinType skinType;
    
    public GameObject skin;

    public Material skinMaterial;

    public int price, skinId;

    public string skinDes;

    public bool locked;
}
