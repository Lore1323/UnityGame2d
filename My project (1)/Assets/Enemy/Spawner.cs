using System.Collections;
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
    
    public TextMeshProUGUI Text;

    public enum SpawnerState { Waiting, Spawning, Cooldown, Initial}
    public SpawnerState currentState = SpawnerState.Initial;

    void Update()
    {
        globalTimer += Time.deltaTime;
        
        switch (currentState)
        {
            case SpawnerState.Initial:
                Text.SetText("¡Busca una torre para activar!");
                break;
            
            case SpawnerState.Waiting:
                Text.SetText("¡Enemigos aproximándose!");
                if (globalTimer >= timeBeforeFirstWave)
                {
                    StartWave();
                    GameManager.Instance.spawner = this;
                }
                break;

            case SpawnerState.Spawning:
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= spawnInterval && enemiesSpawned < enemiesPerWave)
                {
                    SpawnEnemy();
                    spawnTimer = 0f;
                    enemiesSpawned++;
                    Text.SetText("¡Defiende la torre!");
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
                    Text.SetText("¡Torre Activada!");
                }
                break;
        }
    }
    
    void StartWave()
    {
        
        if (currentWave >= maxWaves && AllEnemiesAreInactive())
        {
            Debug.Log("Todas las oleadas completadas");
            torre.DefenseSucesfully();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterTowerCompleted();
                currentState = SpawnerState.Initial;
            }
            StartCoroutine(CompleteAndRemove());
            return;
        }
       

        currentWave++;
        enemiesSpawned = 0;
        globalTimer = 0f;
        isWaveActive = true;
        currentState = SpawnerState.Spawning;    
    }
    
    IEnumerator CompleteAndRemove()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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
