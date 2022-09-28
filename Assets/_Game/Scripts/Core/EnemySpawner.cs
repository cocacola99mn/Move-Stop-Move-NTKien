using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public IndicatorHolder indicatorHolder;
    public Character character, characterHolder;

    public Transform player, spawner;

    [SerializeField]
    float xPos, zPos, randomTimer;

    private Vector3 cacheVector;

    private WaitForSeconds initSpawnWaitTime;

    public void Start()
    {
        OnInit();
    }

    private void Update()
    {
        SetSpawnerPos();
    }

    public void OnInit()
    {
        StartCoroutine(InitSpawn());

        initSpawnWaitTime = new WaitForSeconds(0.5f);
    }

    public void SetSpawnerPos()
    {
        spawner.position = player.position;
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

    public virtual void SpawnEnemy(bool initCheck)
    {
        characterHolder = SimplePool.Spawn<Character>(character, GetRandomPosition(-16, 16), Quaternion.identity);
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
            aiController.indicator.SetIndicatorColor(aiController.bodyColor);
            aiController.indicator.SetIndicatorPoint(aiController.characterPoint);
        }
    }

    public IEnumerator InitSpawn()
    {
        yield return initSpawnWaitTime;

        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy(true);
        }
    }

    public IEnumerator SpawnEnemyOnDead()
    {
        yield return new WaitForSeconds(3);

        SpawnEnemy(false);
    }  
}
