using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Door colliders")]
    [SerializeField] private Collider[] doorColliders;

    [Header("Spawn")]
    [SerializeField] private EnemySpawnManager spawnManager;
    [SerializeField] private int waveCount = 3;
    [SerializeField] private int waveMonsterNum = 15;

    [Header("Close behavior")]
    [SerializeField] private float closeDelay = 0.15f;

    private int currentWave = 0;
    private bool isRunning = false;
    [SerializeField] public bool thisRoomClear = false;

    public bool isOpenDoor = false;
    [SerializeField] private GameObject[] closedDoor;
    [SerializeField] private GameObject[] openDoor;

    private void Start()
    {
        thisRoomClear = false;
        if (spawnManager == null)
        {
            spawnManager = GetComponent<EnemySpawnManager>();
        }
    }

    private void Update()
    {
        switch(isRunning)
        {
            case false:
                if (isOpenDoor) break;
                else OpenDoor(); break;
            case true:
                if(!isOpenDoor) break;
                else CloseDoor(); break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
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
        foreach (var col in doorColliders)
        {
            col.isTrigger = isTrigger;
        }
    }

    private void OpenDoor()
    {
        foreach(var closeDoor in closedDoor)
        {
            closeDoor.gameObject.SetActive(false);
        }

        foreach (var openDoor in openDoor)
        {
            openDoor.gameObject.SetActive(true);
        }
        Debug.Log("중복확인");
        isOpenDoor = true;
    }

    private void CloseDoor()
    {
        foreach (var closeDoor in closedDoor)
        {
            closeDoor.gameObject.SetActive(true);
        }

        foreach (var openDoor in openDoor)
        {
            openDoor.gameObject.SetActive(false);
        }
        Debug.Log("중복확인");

        isOpenDoor = false;
    }
}
