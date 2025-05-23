using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public int spawnAmount = 5;
    
    private float timer;
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval & spawnAmount > 0)
        {
            Spawn();
            timer = 0f;
            spawnAmount -= 1;
        }
    }

    void Spawn()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        
        GameObject Enemy = ObjectPoolManager.SpawnObject(prefab, spawnPoint.position, Quaternion.identity);
        
        Enemy.SetActive(true);
        Enemy.transform.localScale = Vector3.one;
        Enemy.GetComponent<Enemy>().ResetEnemy();


        Debug.Log("Enemigo spawneó en" + spawnPosition.ToString());
        
    }
    
}
