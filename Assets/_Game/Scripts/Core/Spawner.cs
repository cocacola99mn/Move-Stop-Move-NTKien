using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public IndicatorHolder indicatorHolder;
    public Character character, characterHolder;
    public Gift gift;

    public Transform player, spawner;

    [SerializeField]
    float xPos, zPos, randomTimer;

    private Vector3 cacheVector;

    private WaitForSeconds spawnWaitTime;

    public void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        spawnWaitTime = new WaitForSeconds(3);
        InitSpawn();
        SpawnGift();
    }

    public Vector3 GetRandomPosition(float min, float max)
    {
        xPos = Random.Range(min, max);
        zPos = Random.Range(min, max);
        //Avoid enemy spawn too near player
        while(xPos < 10 && zPos< 10 && xPos > -10 && zPos > -10)
        {
            xPos = Random.Range(min, max);
            zPos = Random.Range(min, max);
        }

        cacheVector.x = xPos;
        cacheVector.y = 0;
        cacheVector.z = zPos;
        cacheVector = player.position + cacheVector;
        return cacheVector;
    }

    public void SpawnGift()
    {
        Gift giftDrop = SimplePool.Spawn<Gift>(gift, GetRandomPosition(-16, 16), Quaternion.identity);
        IsOutOfBound(giftDrop.transform);
    }

    public void SpawnEnemy(bool initCheck)
    {
        characterHolder = SimplePool.Spawn<Character>(character, GetRandomPosition(-16, 16), Quaternion.identity);
        IsOutOfBound(characterHolder.transform);
        indicatorHolder.MiddleAttach(characterHolder);
        
        SetEnemyVariable(characterHolder, initCheck);
    }

    public void SetEnemyVariable(Character enemy, bool initCheck)
    {
        //INITCHECK: Check if this spawn is InitSpawn or not
        if (!initCheck)
        {
            AIController aiController = Cache.GetAIController(enemy.gameObject);
            aiController.OnRevive();
        }
    }

    //Check if object is outside the boundary, if true, send enemy inside the boundary
    public void IsOutOfBound(Transform objectTf)
    {
        Vector3 localPos = objectTf.localPosition;
        Vector3 newPos;
        float boundValue = 39;

        if (localPos.x > boundValue || localPos.x < -boundValue || localPos.z > boundValue && localPos.z < -boundValue)
        {
            newPos.x = Random.Range(-15, 15);
            newPos.y = 0;
            newPos.z = Random.Range(-15, 15);
            objectTf.localPosition = newPos;
        }
    }

    // Spawn enemy at beggining of the game
    public void InitSpawn()
    {
        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy(true);
        }
    }

    //Spawn enemy when an enemy die
    public IEnumerator SpawnEnemyOnDead()
    {
        yield return spawnWaitTime;

        SpawnEnemy(false);
    }  
}
