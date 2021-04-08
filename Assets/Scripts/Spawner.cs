using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;

    public float startTimeBtwSpawns;
    public float minTimeBetweenSpawns;
    public float decrease;
    public GameObject player;

    private float timeBtwSpawns;

    void Update()
    {
        if (player != null)
        {
            if (timeBtwSpawns <= 0)
            {
                Transform randowmSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                GameObject randomEnemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];

                Instantiate(randomEnemy, randowmSpawnPoint.position, Quaternion.identity);

                print(randowmSpawnPoint.position);

                if (startTimeBtwSpawns > minTimeBetweenSpawns)
                {
                    startTimeBtwSpawns -= decrease;
                }

                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
