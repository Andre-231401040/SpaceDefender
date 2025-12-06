using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject meteorKecilPrefab;
    public GameObject meteorBesarPrefab; // Asteroid (tembak 10 kali)

    [Header("Settings")]
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float randomX = Random.Range(min.x + 0.5f, max.x - 0.5f);
        Vector2 spawnPos = new Vector2(randomX, max.y + 1f);


        GameObject meteorToSpawn;

        if (Random.value > 0.7f)
        {
            meteorToSpawn = meteorBesarPrefab;
        }
        else
        {
            meteorToSpawn = meteorKecilPrefab;
        }

        if (meteorToSpawn != null)
        {
            Instantiate(meteorToSpawn, spawnPos, Quaternion.identity);
        }
    }
}