using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform playerTarget;
    public Transform fallbackTarget; // por ejemplo, una torre u objetivo base
    public ObjectToDefense torre;
    public Transform[] spawnPoints;

    public float timeBeforeFirstWave = 30f;
    public float timeBetweenWaves = 60f;
    public float spawnInterval = 3f;
    public int enemiesPerWave = 5;
    public int maxWaves = 5;

    public float globalTimer = 0f;
    private float spawnTimer = 0f;

    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private bool isWaveActive = false;
    
    public Timer timer;

    private enum SpawnerState { Waiting, Spawning, Cooldown }
    private SpawnerState currentState = SpawnerState.Waiting;

    void Update()
    {
        globalTimer += Time.deltaTime;
        
        switch (currentState)
        {
            case SpawnerState.Waiting:
                ActivateSpawner();
                timer.countdownActive = true;
                if (globalTimer >= timeBeforeFirstWave)
                {
                    StartWave();
                    timer.countdownActive = false;
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
    
    public void ActivateSpawner()
    {
        timer.StartCountdown(timeBeforeFirstWave, this);
    }
    

    void StartWave()
    {
        
        if (currentWave >= maxWaves && AllEnemiesAreInactive())
        {
            Debug.Log("Todas las oleadas completadas");
            currentState = SpawnerState.Waiting; // O podr�as desactivar el Spawner con `enabled = false;`
            torre.DefenseSucesfully();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterTowerCompleted();
            }
            Destroy(gameObject);
            return;
        }
       

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
    public bool AllEnemiesAreInactive()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(true); // 'true' incluye inactivos

        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                return false; // Si al menos uno está activo, no se cumple
            }
        }

        return true; // Todos están desactivados
    }
}
