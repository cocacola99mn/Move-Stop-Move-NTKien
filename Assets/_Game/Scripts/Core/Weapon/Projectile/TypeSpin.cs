using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSpin : ProjectileController
{
    public override void TransformProjectile()
    {
        base.TransformProjectile();
        projectileTransform.Rotate(0, 0, 10, Space.Self);
    }
}
