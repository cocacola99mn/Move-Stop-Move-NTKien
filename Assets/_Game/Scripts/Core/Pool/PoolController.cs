using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public ItemListData itemListData;

    public List<Transform> parentTransform;
    public Transform enemyHolder, indicatorHolder, giftHolder;

    public List<ProjectileController> projectileControllers;
    public Character enemy;
    public Indicator indicator;
    public Gift gift;

    /*public Transform tf_vfx;
    public ParticleSystem hitvfx;*/

    private void Awake()
    {
        /*ParticlePool.Preload(hitvfx, 10, tf_vfx);*/
        OnInit();
    }

    public void OnInit()
    {
        SimplePool.Preload(enemy, 20, enemyHolder);
        SimplePool.Preload(indicator, 10, indicatorHolder);
        SimplePool.Preload(gift, 5, giftHolder);

        for (int i = 0; i < itemListData.projectileList.Count; i++)
        {
            projectileControllers.Add(Cache.GetProjectileController(itemListData.projectileList[i].projectile));
            SimplePool.Preload(projectileControllers[i], 15, parentTransform[i]);
        }
    }
}
