using System.Collections;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    
    //public ObjectPool pool;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnInterval;
    
    private int _spawnedCount;
    
    private IEnumerator destroySpawnableCoroutine;

    
    public int SpawnedCount
    {
        get => _spawnedCount;
        set => _spawnedCount = value;
    }

    public float SpawnDelay
    {
        get => _spawnDelay;
        set => _spawnDelay = value;
    }

    public float SpawnInterval
    {
        get => _spawnInterval;
        set => _spawnInterval = value;
    }




    
    // Start is called before the first frame update
    
    /*
    public int GetSpawnedCount()
    {
        return _spawnedCount;
    }
    

    public void UpdateSpawnables(params GameObject[] list)
    {
        pool.DestroyAll();
        pool.WakeAllObjectsAs(list);
    }

    public bool AreEnemiesAlive()
    {
        return pool.isAliveOnPool();
    }

    protected void InitializePool()
    {
        spawnerData.pool = Utility.FindWithTag(transform, "Pool").GetComponent<ObjectPool>();
    }

    public void DisableActives()
    {
        destroySpawnableCoroutine = spawnerData.pool.SleepActiveObjects();
        StartCoroutine(destroySpawnableCoroutine);
    }*/

    
    
    public abstract IEnumerator Spawn(int maxSpawnCount, GameObject spawnTarget = null);
}