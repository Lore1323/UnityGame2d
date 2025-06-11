using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform playerTarget;
    public Transform fallbackTarget; // por ejemplo, una torre u objetivo base

    public Transform[] spawnPoints;

    public float timeBeforeFirstWave = 30f;
    public float timeBetweenWaves = 60f;
    public float spawnInterval = 3f;
    public int enemiesPerWave = 5;

    private float globalTimer = 0f;
    private float spawnTimer = 0f;

    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private bool isWaveActive = false;

    private enum SpawnerState { Waiting, Spawning, Cooldown }
    private SpawnerState currentState = SpawnerState.Waiting;

    void Update()
    {
        globalTimer += Time.deltaTime;

        switch (currentState)
        {
            case SpawnerState.Waiting:
                if (globalTimer >= timeBeforeFirstWave)
                {
                    StartWave();
                }
                break;

            case SpawnerState.Spawning:
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= spawnInterval && enemiesSpawned < enemiesPerWave)
                {
                    SpawnEnemy();
                    spawnTimer = 0f;
                    enemiesSpawned++;
                }

                if (enemiesSpawned >= enemiesPerWave)
                {
                    currentState = SpawnerState.Cooldown;
                    globalTimer = 0f;
                }
                break;

            case SpawnerState.Cooldown:
                if (globalTimer >= timeBetweenWaves)
                {
                    StartWave();
                }
                break;
        }
    }

    void StartWave()
    {
        currentWave++;
        enemiesSpawned = 0;
        globalTimer = 0f;
        isWaveActive = true;
        currentState = SpawnerState.Spawning;
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = ObjectPoolManager.SpawnObject(prefab, spawnPoint.position, Quaternion.identity);

        enemy.SetActive(true);
        enemy.transform.localScale = Vector3.one;

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.playerTarget = playerTarget;
        enemyScript.target = fallbackTarget;

        enemyScript.ResetEnemy();
        enemyScript.SetStats(currentWave);
    }
}
