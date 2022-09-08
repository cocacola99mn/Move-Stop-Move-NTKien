using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projecTileSpeed, existTime;

    void Start()
    {
        projecTileSpeed = 10;
        existTime = 3;
    }

    void Update()
    {
        MovingProjectile();
        ProjectileLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit(other);
    }

    public void MovingProjectile()
    {
        transform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
    }

    public void ProjectileLifeTime()
    {
        existTime -= Time.deltaTime;

        if (existTime <= 0)
        {
            ObjectPooling.Ins.Despawn(WeaponManager.Ins.GetWeaponPref() + "", gameObject);
            existTime = 3;
        }    
            
    }

    public void OnProjectileHit(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.DAMAGEABLE_TAG))
        {
            ObjectPooling.Ins.Despawn(WeaponManager.Ins.GetWeaponPref() + "", gameObject);
            existTime = 3;
            Destroy(other.gameObject);
        }
    }

}
