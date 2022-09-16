using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public Transform player, spawner;

    public float xPos, zPos;

    public float timer, secondsFloatTimer, randomTimer;

    private void Update()
    {
        InitSpawn();
        SetSpawnerPos();
    }

    public void InitSpawn()
    {
        if (ObjectPooling.Ins.checkInitSpawn == true)
        {
            for (int i = 0; i < 8; i++)
            {
                ObjectPooling.Ins.Spawn(GameConstant.ENEMY_POOLING, GetRandomPosition(-16, 16), Quaternion.identity);
            }

            ObjectPooling.Ins.checkInitSpawn = false;
        }
    }

    public void SetSpawnerPos()
    {
        spawner.position = player.position;
    }

    public void GetRandomTimer()
    {
        randomTimer = Random.Range(0.5f, 2);
    }

    public Vector3 GetRandomPosition(float min, float max)
    {
        xPos = Random.Range(min, max);
        zPos = Random.Range(min, max);

        while(xPos < 8 && zPos< 8 && xPos > -8 && zPos > -8)
        {
            xPos = Random.Range(min, max);
            zPos = Random.Range(min, max);
        }

        return new Vector3(xPos, 0, zPos);
    }

    public IEnumerator SpawnEnemy()
    {
        GetRandomTimer(); 
        
        yield return new WaitForSeconds(randomTimer);
       
        GameObject enemy = ObjectPooling.Ins.Spawn(GameConstant.ENEMY_POOLING, GetRandomPosition(-16, 16), Quaternion.identity);

        if (GetComponent<AIController>() != null)
            enemy.GetComponent<AIController>().isDead = false;
    }
}
