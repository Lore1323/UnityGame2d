using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    
    public int initialEnemies = 3;
    public int currentWave = 1;
    public int totalWaves= 5;
    public float timeBetweenWaves= 5f;
    public float timeBetweenSpawns= 0.5f;
    public float waveMultiplier= 0;
    public int enemiesAlive= 0;
    
    private Spawner currentSpawner;

    private void Awake()
    {
        instance = this;
    }

    public void StartWave(Spawner spawner)
    {
        if (totalWaves >= currentWave)
        {
            currentWave++;
        }
        
        currentSpawner = spawner;
        
        int amount = currentWave * 2;
    }
}
