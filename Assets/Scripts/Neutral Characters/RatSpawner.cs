using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject ratPrefab;
    float spawnInterval = 40f;
    float ratLife;
    float timeSinceLastSpawn = 0f;
    Vector2 chunkPosition;
    void Start()
    {
        chunkPosition = new Vector2(
            Mathf.FloorToInt(transform.position.x / 20) * 20,
            Mathf.FloorToInt(transform.position.y / 20) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= spawnInterval)
        {
            SpawnRat();
            timeSinceLastSpawn= 0f;
        }
    }
    void SpawnRat()
    {
        float randomX = Random.Range(chunkPosition.x, chunkPosition.x + 20);
        float randomY = Random.Range(chunkPosition.y, chunkPosition.y + 20);
        GameObject newRat = Instantiate(ratPrefab, new Vector2(randomX, randomY), Quaternion.identity);
    }
}
