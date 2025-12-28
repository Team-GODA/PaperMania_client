using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public int MonsterCount;
    [SerializeField] private GameObject enemyPrefeb;
    [SerializeField] private int initialSize = 20;

    [SerializeField] private GameObject minPos;
    [SerializeField] private GameObject maxPos;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private int activeEnemies = 0;

    public event Action OnAllEnemiesCleared;

    public void Enqueue(GameObject obj) => pool.Enqueue(obj);

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var b = Instantiate(enemyPrefeb, transform);
            b.SetActive(false);
            pool.Enqueue(b);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnMonster(MonsterCount);
        }
    }

    public void SpawnMonster(int enemyCount)
    {
        if (enemyCount <= 0) return;

        activeEnemies += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            GetEnemy(RandomSpawnPoint(), transform.rotation);
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            var extra = Instantiate(enemyPrefeb, transform);
            extra.SetActive(false);
            pool.Enqueue(extra);
        }

        var enemyGo = pool.Dequeue();
        enemyGo.transform.SetPositionAndRotation(position, rotation);

        var enemyComp = enemyGo.GetComponent<Enemy>();
        if (enemyComp != null)
        {
            enemyComp.OnDied -= HandleEnemyDeath;
            enemyComp.OnDied += HandleEnemyDeath;
        }

        enemyGo.SetActive(true);
        return enemyGo;
    }

    private void HandleEnemyDeath(Enemy e)
    {
        e.OnDied -= HandleEnemyDeath;

        Enqueue(e.gameObject);

        activeEnemies = Mathf.Max(0, activeEnemies - 1);

        if (activeEnemies == 0)
        {
            OnAllEnemiesCleared?.Invoke();
        }
    }

    Vector3 RandomSpawnPoint()
    {
        float x = UnityEngine.Random.Range(minPos.transform.position.x, maxPos.transform.position.x);
        float z = UnityEngine.Random.Range(minPos.transform.position.z, maxPos.transform.position.z);
        float y = minPos.transform.position.y;
        return new Vector3(x, y, z);
    }
}
