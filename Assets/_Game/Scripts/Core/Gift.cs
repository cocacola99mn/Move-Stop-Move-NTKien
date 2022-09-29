using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : GameUnit
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.DAMAGEABLE_TAG))
        {
            LevelManager.Ins.spawner.SpawnGift();
            SimplePool.Despawn(this);
            if (!Cache.GetCharacter(other).weaponBoost)
            {
                Cache.GetCharacter(other).OnWeaponBoost(2, true);
            }
        }
        else
        {
            SimplePool.Despawn(this);
        }
    }
}
