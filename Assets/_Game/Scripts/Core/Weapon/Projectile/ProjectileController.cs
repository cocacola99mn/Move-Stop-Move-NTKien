using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Character bulletShooter;

    public Projectile projectile;
    
    public float projecTileSpeed, projectileExistTime;

    public Transform projectileTransform;

    void Start()
    {
        projecTileSpeed = 5;
        projectileExistTime = 1.5f;
    }

    void Update()
    {
        TransformProjectile();
        ProjectileLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit(other);
    }

    public void ProjectileLifeTime()
    {
        projectileExistTime -= Time.deltaTime;

        if (projectileExistTime <= 0)
        {
            DespawnProjectile();

            projectileExistTime = 1.5f;
        }                
    }

    public void OnProjectileHit(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.DAMAGEABLE_TAG))
        {
            DespawnProjectile();

            projectileExistTime = 1.5f;

            bulletShooter.OnGetKill(Cache.GetCharacter(other));
        }
    }

    public virtual void DespawnProjectile()
    {
        ObjectPooling.Ins.Despawn(projectile.id + "", gameObject);
    }

    public virtual void TransformProjectile()
    {
        transform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
    }
}
