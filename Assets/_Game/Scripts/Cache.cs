using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    private static Dictionary<Collider, Character> character = new Dictionary<Collider, Character>();
    private static Dictionary<GameObject, ProjectileController> projectileController = new Dictionary<GameObject, ProjectileController>();

    public static Character GetCharacter(Collider collider)
    {
        if (!character.ContainsKey(collider))
            character.Add(collider, collider.GetComponent<Character>());

        return character[collider];
    }

    public static ProjectileController GetProjectileController(GameObject gameObject)
    {
        if (!projectileController.ContainsKey(gameObject))
            projectileController.Add(gameObject, gameObject.GetComponent<ProjectileController>());

        return projectileController[gameObject];
    }
}
