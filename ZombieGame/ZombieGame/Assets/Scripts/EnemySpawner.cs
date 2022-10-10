using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public GameObject enemyPrefab;
    /*
    private void Awake()
    {
        InitializePool();
    }

    /*public override IEnumerator Spawn(int maxSpawnCount, GameObject spawnTarget = null)
    {
        yield return spawnerData.Spawn(maxSpawnCount);
    }*/

    public void Start()
    {
        StartCoroutine(Spawn(30));
    }

    public override IEnumerator Spawn(int maxSpawnCount, GameObject spawnTarget = null)
    {
        SpawnedCount = 0;
        yield return new WaitForSeconds(SpawnDelay);
        while (SpawnedCount < maxSpawnCount)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            SpawnedCount++;
            Utils.SetSpawnLocation(enemy);
            yield return new WaitForSeconds(SpawnInterval);
        }

        yield return null;
    }
}