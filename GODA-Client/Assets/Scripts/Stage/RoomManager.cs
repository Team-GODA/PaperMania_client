using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Door colliders (set Collider2D of each door)")]
    [SerializeField] private Collider2D[] doorColliders;

    [Header("Spawn")]
    [SerializeField] private EnemySpawnManager spawnManager;
    [SerializeField] private int waveCount = 3;
    [SerializeField] private int waveMonsterNum = 15;

    [Header("Close behavior")]
    [SerializeField] private float closeDelay = 0.15f;

    private int currentWave = 0;
    private bool isRunning = false;
    [SerializeField] private bool thisRoomClear = false;

    private void Start()
    {
        thisRoomClear = false;
        if (spawnManager == null)
        {
            spawnManager = GetComponent<EnemySpawnManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (isRunning) return;
        if (thisRoomClear) return;

        StartWaves();
    }

    private void StartWaves()
    {
        spawnManager.OnAllEnemiesCleared += HandleWaveCleared;

        currentWave = 0;
        isRunning = true;

        StartCoroutine(CloseDoorsAfterDelay(closeDelay));

        SpawnNextWave();
        Debug.Log("RoomManager: Waves started.");
    }

    private IEnumerator CloseDoorsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetDoorsTrigger(false); 
        Debug.Log("RoomManager: Doors closed.");
    }

    private void SpawnNextWave()
    {
        if (currentWave >= waveCount)
        {
            FinishAllWaves();
            return;
        }

        currentWave++;
        spawnManager.SpawnMonster(waveMonsterNum);
        Debug.Log($"RoomManager: Spawned wave {currentWave}/{waveCount} ({waveMonsterNum} monsters).");
    }

    private void HandleWaveCleared()
    {
        Debug.Log($"RoomManager: Wave {currentWave} cleared.");
        SpawnNextWave();
    }

    private void FinishAllWaves()
    {
        if (spawnManager != null)
            spawnManager.OnAllEnemiesCleared -= HandleWaveCleared;

        isRunning = false;
        SetDoorsTrigger(true);
        thisRoomClear = true;
    }

    private void SetDoorsTrigger(bool isTrigger)
    {
        if (doorColliders == null || doorColliders.Length == 0) return;

        foreach (var col in doorColliders)
        {
            if (col == null) continue;
            col.isTrigger = isTrigger;
        }
    }
}
