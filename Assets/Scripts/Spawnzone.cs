using System;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
    private Collider2D zone;
    private bool shouldSpawnEnemies;

    public GameObject enemy;
    
    public float spawnTimer;
    public float defaultSpawnTimer = 5;
    

    private void Start()
    {
        zone = GetComponent<Collider2D>();
        shouldSpawnEnemies = true;
    }

    private void FixedUpdate()
    {
        if (shouldSpawnEnemies)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Vector2 spawnLoc = zone.GetRandomPointInside();
                Instantiate(enemy, spawnLoc, Quaternion.identity);
                spawnTimer = defaultSpawnTimer;
            }
        }
    }
}
