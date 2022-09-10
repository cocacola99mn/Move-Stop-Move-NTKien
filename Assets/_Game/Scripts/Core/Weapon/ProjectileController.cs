using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Projectile projectile;
    
    public float projecTileSpeed, existTime;

    public Transform weapon;

    void Start()
    {
        projecTileSpeed = 5;
        existTime = 3;
    }

    void Update()
    {
        GetProjectileType();
        ProjectileLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit(other);
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

    public void MovingProjectile()
    {
        transform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
    }

    public void TypeSpin()
    {
        weapon.Rotate(0, 0, 10, Space.Self);
    }

    public void GetProjectileType()
    {
        switch (projectile.weaponType)
        {
            case WeaponType.Spin:
                MovingProjectile();
                TypeSpin();
                break;
            case WeaponType.Split:
                MovingProjectile();
                break;
            default:
                //Type: Normal
                MovingProjectile();
                break;
        }
    }

}
