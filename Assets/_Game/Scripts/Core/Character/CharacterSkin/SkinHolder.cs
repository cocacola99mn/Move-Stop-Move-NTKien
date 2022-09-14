using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinHolder : Singleton<SkinHolder>
{
    public GameObject hatHolder, pant, mySkin;
    public SkinnedMeshRenderer pantMesh;
    public int currentHat, currentPant;

    public List<Skin> hatHolderList;
    public List<GameObject> hatList;
    public List<Material> pantList;

    private void Start()
    {
        pantMesh = pant.GetComponent<SkinnedMeshRenderer>();

        GetSkinPref();
        InitPrefSkin();
    }

    public void InitPrefSkin()
    {
        hatList[currentHat].SetActive(true);
        
        if(currentPant != 9998)
            pantMesh.material = pantList[currentPant];
    }

    public void GetSkinPref()
    {
        if (SkinManager.Ins.GetHatPref() == 0)
            currentHat = currentPant = 9999;
        else
        {
            currentHat = SkinManager.Ins.GetHatPref() - 1;
            currentPant = SkinManager.Ins.GetPantPref() - 1;
        }
    }
}
