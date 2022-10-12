using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{   
    public Indicator indicator;

    public GameObject holder, indicatorObject;
    public Transform holderTransform;
    Vector3 initSpawnPoint = new Vector3(10, 10, 10);

    //Middle man help attach Indicator and AIController
    public void MiddleAttach(Character character)
    {
        Indicator indicator = SimplePool.Spawn<Indicator>(this.indicator, initSpawnPoint, Quaternion.identity);
        indicator.AttachCharacter(character);
        character.indicator = indicator;
    }
}
