using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public IndicatorHolder indicatorHolder;

    public Transform player, spawner;

    [SerializeField]
    float xPos, zPos, randomTimer;

    private Vector3 cacheVector;

    private WaitForSeconds randomWaitTime, initSpawnWaitTime;

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

        randomWaitTime = new WaitForSeconds(randomTimer);
        initSpawnWaitTime = new WaitForSeconds(0.5f);
    }

    public void SetSpawnerPos()
    {
        spawner.position = player.position;
    }

    public void GetRandomTimer()
    {
        randomTimer = Random.Range(1, 3);
    }

    public Vector3 GetRandomPosition(float min, float max)
    {
        xPos = Random.Range(min, max);
        zPos = Random.Range(min, max);

        //To avoid enemy spawn too near player
        while(xPos < 10 && zPos< 10 && xPos > -10 && zPos > -10)
        {
            xPos = Random.Range(min, max);
            zPos = Random.Range(min, max);
        }

        cacheVector.x = xPos;
        cacheVector.y = 0;
        cacheVector.z = zPos;

        return cacheVector;
    }

    public virtual void SpawnEnemy(bool initCheck)
    {
        GameObject enemy = ObjectPooling.Ins.Spawn(GameConstant.ENEMY_POOLING, GetRandomPosition(-16, 16), Quaternion.identity);
        GameObject objectHolder = indicatorHolder.PopIndicatorFromPool();
        indicatorHolder.MiddleAttach(objectHolder, Cache.GetAIController(enemy));

        SetEnemyVariable(enemy, initCheck);
    }

    public void SetEnemyVariable(GameObject enemy, bool initCheck)
    {
        //INITCHECK: Check if this spawn is InitSpawn or not
        if (!initCheck)
        {
            AIController aiController = Cache.GetAIController(enemy);
            if (aiController != null)
            {
                aiController.isDead = false;
                aiController.controller.enabled = true;
                aiController.indicator.SetIndicatorColor(aiController.bodyColor);
                aiController.indicator.SetIndicatorPoint(aiController.characterPoint);
            }
        }
    }

    public IEnumerator InitSpawn()
    {
        yield return initSpawnWaitTime;

        indicatorHolder.InitIndicator();

        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy(true);
        }
    }

    public IEnumerator SpawnEnemyOnDead()
    {
        GetRandomTimer();

        yield return randomWaitTime;

        SpawnEnemy(false);
    }  
}
