using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public Wave[] waves;
    public GameObject[] enemyPrefab;

    public float timeCodownWaves = 5f;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(3f);

        for (int w = 0; w < waves.Length; w++)
        {

            Wave currentWave = waves[w];


            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                int enemy = Random.Range(0, enemyPrefab.Length);

                Instantiate(enemyPrefab[enemy], spawn.position, Quaternion.identity);

                yield return new WaitForSeconds(currentWave.spawnDelay);
            }

            yield return new WaitForSeconds(timeCodownWaves);
        }

        Debug.Log("All waves finished!");
    }
}