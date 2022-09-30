using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public ItemListData itemListData;

    #region OBJECT
    public List<Transform> parentTransform;
    public Transform enemyHolder, indicatorHolder, giftHolder;

    public List<ProjectileController> projectileControllers;
    public Character enemy;
    public Indicator indicator;
    public Gift gift;
    #endregion

    #region PARTICLE
    public Transform tf_hitVfx;
    public Transform tf_levelUpVfx;
    public ParticleSystem hitVfx;
    public ParticleSystem levelUpVfx;
    #endregion

    private void Awake()
    {
        
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

        ParticlePool.Preload(hitVfx, 10, tf_hitVfx);
        ParticlePool.Preload(levelUpVfx, 2, tf_levelUpVfx);
    }
}
