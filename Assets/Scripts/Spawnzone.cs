using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Constants;

public class Spawnzone : MonoBehaviour
{
    private Collider2D zone;
    private bool shouldSpawnEnemies;

    [SerializeField] List<GameObject> spawnableEnemies;
    [SerializeField] List<int> enemySpawnChances;
    
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
                spawnTimer = defaultSpawnTimer;

                GameObject enemyToSpawn = spawnableEnemies[0]; 
                int randomNumber = Random.Range(0, 100);
                for (int i = 0; i < enemySpawnChances.Count; i++)
                {
                    if (randomNumber < enemySpawnChances[i])
                    {
                        enemyToSpawn = spawnableEnemies[i];
                        break;
                    }
                    randomNumber -= enemySpawnChances[i];
                }

                Instantiate(enemyToSpawn, spawnLoc, Quaternion.identity);
            }
        }
    }
}
