using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public PlayerController player;

    public PlayerDataSO playerDataSO;

    public ItemListData itemListData;

    public List<GameObject> weaponObjectList, projectileObjectList, hatObjectList, pantObjectList;

    public List<Material> pantMaterialList;

    public List<Color32> colorList;

    public void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        InitDataList();
    }

    public void InitDataList()
    {
        for (int i = 0; i < itemListData.weaponList.Count; i++)
        {
            weaponObjectList.Add(itemListData.weaponList[i].weapon);
            projectileObjectList.Add(itemListData.projectileList[i].projectile);
        }

        for (int i = 0; i < itemListData.hatDatasList.Count; i++)
        {
            hatObjectList.Add(itemListData.hatDatasList[i].hat);
        }

        for (int i = 0; i < itemListData.pantDatasList.Count; i++)
        {
            pantObjectList.Add(itemListData.pantDatasList[i].pant);
            pantMaterialList.Add(itemListData.pantDatasList[i].pantMaterial);
        }
    }

    public void SetIntData(string data, ref int dataValue, int value)
    {
        dataValue = value;
        PlayerPrefs.SetInt(data, value);
    }

    public void SetFloatData(string data, ref float dataValue, float value)
    {
        dataValue = value;
        PlayerPrefs.SetFloat(data, value);
    }

    public void SetStringData(string data, ref string dataValue, string value)
    {
        dataValue = value;
        PlayerPrefs.SetString(data, value);
    }

    public void SetBoolData(string data, ref bool dataValue, bool value)
    {
        dataValue = value;
        PlayerPrefs.SetInt(data, value ? 1 : 0);
    }
}
