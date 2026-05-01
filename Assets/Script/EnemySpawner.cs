using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Wave[] waves;

    public float timeBetweenWaves = 5f;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(3f);

        for (int w = 0; w < waves.Length; w++)
        {
            Wave wave = waves[w];

            Debug.Log("Wave " + (w + 1));

            
            List<GameObject> spawnList = new List<GameObject>();

            for (int i = 0; i < wave.enemyPrefabs.Length; i++)
            {
                for (int j = 0; j < wave.counts[i]; j++)
                {
                    spawnList.Add(wave.enemyPrefabs[i]);
                }
            }

            
            for (int i = 0; i < spawnList.Count; i++)
            {
                int rand = Random.Range(0, spawnList.Count);

                GameObject temp = spawnList[i];
                spawnList[i] = spawnList[rand];
                spawnList[rand] = temp;
            }

            
            foreach (GameObject enemy in spawnList)
            {
                Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(enemy, spawn.position, Quaternion.identity);

                yield return new WaitForSeconds(wave.spawnDelay);
            }

            // ⏳ รอเวฟถัดไป
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves finished!");
    }
}