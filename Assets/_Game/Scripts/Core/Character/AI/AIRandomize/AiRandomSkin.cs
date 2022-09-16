using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRandomSkin : MonoBehaviour
{
    public List<GameObject> hatList;
    public List<Material> pantList;
    public List<Material> bodyColorList;

    public SkinnedMeshRenderer pantMaterial,aiBody;

    public int randomIndex;
    void Start()
    {
        getRandomHat();
        getRandomPant();
        getRandomBody();
    }

    public void getRandomHat()
    {        
        hatList[getRandomIndex(hatList.Count - 1)].SetActive(true);
    }

    public void getRandomPant()
    {
        pantMaterial.material = pantList[getRandomIndex(pantList.Count - 1)];
    }

    public void getRandomBody()
    {
        aiBody.material = bodyColorList[getRandomIndex(bodyColorList.Count - 1)];
    }

    public int getRandomIndex(int index)
    {
        randomIndex = Random.Range(0, index);
        return randomIndex;
    }
}
