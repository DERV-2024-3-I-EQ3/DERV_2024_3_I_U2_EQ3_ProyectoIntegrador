using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 1f;
    public float spawnRangeX = 10f;
    public float spawnPosZ = 20f;

    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, spawnRate);
    }

    void SpawnAsteroid()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, 0, spawnPosZ);
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }
}
