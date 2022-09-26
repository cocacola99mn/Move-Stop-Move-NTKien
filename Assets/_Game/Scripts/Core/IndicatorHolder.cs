using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    public GameObject holder, indicatorObject;

    public Transform holderTransform;

    public Indicator indicator;

    //Middle man help attach Indicator and AIController
    public void MiddleAttach(Character character)
    {
        Indicator indicator = SimplePool.Spawn<Indicator>(this.indicator, Vector3.zero, Quaternion.identity);
        indicator.AttachCharacter(character);
        character.indicator = indicator;
    }
}
