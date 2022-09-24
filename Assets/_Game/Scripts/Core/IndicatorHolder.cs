using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    public GameObject holder, indicatorObject;

    public Transform holderTransform;

    private Dictionary<AIController, Indicator> indicatorDict = new Dictionary<AIController, Indicator>();

    public void InitIndicator()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject objectHolder = PopIndicatorFromPool();
            objectHolder.transform.SetParent(holderTransform);
            Cache.GetIndicator(objectHolder).DespawnIndicator();
        }
    }

    //Middle man help attach Indicator and AIController
    public void MiddleAttach(GameObject objectHolder, AIController aIController)
    {
        Indicator indicator = Cache.GetIndicator(objectHolder);

        indicator.AttachAIController(aIController);
        aIController.AttachIndicator(indicator);
    }

    public GameObject PopIndicatorFromPool()
    {
        GameObject objectHolder = ObjectPooling.Ins.Spawn(GameConstant.INDICATOR_POOLING, Vector3.zero, Quaternion.identity);
        return objectHolder;
    }

    public void HideIndicatorHolder()
    {
        holder.SetActive(false);
    }
}
