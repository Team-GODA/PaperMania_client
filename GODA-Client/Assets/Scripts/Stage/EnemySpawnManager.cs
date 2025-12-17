using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefeb;
    [SerializeField] private int initialSize = 20;

    [SerializeField] private GameObject xyMin;
    [SerializeField] private GameObject xyMax;

    private Queue<GameObject> pool = new Queue<GameObject>();
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
            GetEnemy(RandomSpawnPoint(), gameObject.transform.rotation);
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

        var enemy = pool.Dequeue();
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.SetActive(true);
        return enemy;
    }

    Vector2 RandomSpawnPoint()
    {
        float x = Random.Range(xyMin.transform.position.x, xyMax.transform.position.x);
        float y = Random.Range(xyMin.transform.position.y, xyMax.transform.position.y);
        return new Vector2(x, y);
    }
}
