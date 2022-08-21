using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Normal, Spin, Split }

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Projectile")]
public class Projectile : ScriptableObject
{
    public GameObject projectile;

    public float attackSpeed, attackRange;

    public WeaponType weaponType;
}
