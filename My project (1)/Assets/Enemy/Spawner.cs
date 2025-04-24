using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    
    private float timer;
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        
        GameObject Enemy = ObjectPoolManager.SpawnObject(prefab, spawnPoint.position, Quaternion.identity);
        
        Enemy.SetActive(true);
        Enemy.transform.localScale = Vector3.one;
        
        Debug.Log("Enemigo spawneó en" + spawnPosition.ToString());
        
    }
    
}
