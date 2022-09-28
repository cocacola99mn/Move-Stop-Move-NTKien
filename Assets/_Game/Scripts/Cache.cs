using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    private static Dictionary<Collider, Character> character = new Dictionary<Collider, Character>();
    private static Dictionary<GameObject, ProjectileController> projectileController = new Dictionary<GameObject, ProjectileController>();
    private static Dictionary<GameObject, AIController> aiController = new Dictionary<GameObject, AIController>();
    private static Dictionary<GameObject, PlayerController> playerController = new Dictionary<GameObject, PlayerController>();
    private static Dictionary<GameObject, RectTransform> rectTransform = new Dictionary<GameObject, RectTransform>();
    private static Dictionary<GameObject, GetItemData> getItemData = new Dictionary<GameObject, GetItemData>();
    private static Dictionary<GameObject, TypeSplit> getTypeSplit = new Dictionary<GameObject, TypeSplit>();
    private static Dictionary<GameObject, Indicator> getIndicator = new Dictionary<GameObject, Indicator>();

    public static Character GetCharacter(Collider collider)
    {
        if (!character.ContainsKey(collider))
        {
            character.Add(collider, collider.GetComponent<Character>());
        }

        return character[collider];
    }

    public static ProjectileController GetProjectileController(GameObject gameObject)
    {
        if (!projectileController.ContainsKey(gameObject))
        {
            projectileController.Add(gameObject, gameObject.GetComponent<ProjectileController>());
        }

        return projectileController[gameObject];
    }

    public static AIController GetAIController(GameObject gameObject)
    {
        if (!aiController.ContainsKey(gameObject))
        {
            aiController.Add(gameObject, gameObject.GetComponent<AIController>());
        }

            return aiController[gameObject];     
    }

    public static PlayerController GetPlayerController(GameObject gameObject)
    {
        if (!aiController.ContainsKey(gameObject))
        {
            playerController.Add(gameObject, gameObject.GetComponent<PlayerController>());
        }

        return playerController[gameObject];
    }

    public static RectTransform GetRectTransform(GameObject gameObject)
    {
        if (!rectTransform.ContainsKey(gameObject))
        {
            rectTransform.Add(gameObject, gameObject.GetComponent<RectTransform>());
        }

        return rectTransform[gameObject];
    }

    public  static GetItemData GetItemData(GameObject gameObject)
    {
        if (!getItemData.ContainsKey(gameObject))
        {
            getItemData.Add(gameObject, gameObject.GetComponent<GetItemData>());
        }

        return getItemData[gameObject];
    }

    public static TypeSplit GetTypeSplit(GameObject gameObject)
    {
        if (!getTypeSplit.ContainsKey(gameObject))
        {
            getTypeSplit.Add(gameObject, gameObject.GetComponent<TypeSplit>());
        }

        return getTypeSplit[gameObject];
    }

    public static Indicator GetIndicator(GameObject gameObject)
    {
        if (!getIndicator.ContainsKey(gameObject))
        {
            getIndicator.Add(gameObject, gameObject.GetComponent<Indicator>());
        }

        return getIndicator[gameObject];
    }
}
