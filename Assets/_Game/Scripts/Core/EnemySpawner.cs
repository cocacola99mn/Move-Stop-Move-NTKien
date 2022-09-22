using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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

    public IEnumerator InitSpawn()
    {
        yield return initSpawnWaitTime;

        for (int i = 0; i < 8; i++)
        {
            ObjectPooling.Ins.Spawn(GameConstant.ENEMY_POOLING, GetRandomPosition(-16, 16), Quaternion.identity);
        }
    }

    public IEnumerator SpawnEnemy()
    {
        GetRandomTimer();

        yield return randomWaitTime;
       
        GameObject enemy = ObjectPooling.Ins.Spawn(GameConstant.ENEMY_POOLING, GetRandomPosition(-16, 16), Quaternion.identity);

        if (Cache.GetAIController(enemy) != null)
        {
            Cache.GetAIController(enemy).isDead = false;
            Cache.GetAIController(enemy).controller.enabled = true;
        }
    }
}
