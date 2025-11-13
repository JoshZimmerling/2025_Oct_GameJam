using System.Collections.Generic;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
    private Collider2D zone;

    [SerializeField] List<GameObject> spawnableEnemies;
    [SerializeField] List<int> enemySpawnChances;

    private void Start()
    {
        zone = GetComponent<Collider2D>();
    }

    public void spawnEnemy()
    {
        Vector2 spawnLoc = zone.GetRandomPointInside();

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
