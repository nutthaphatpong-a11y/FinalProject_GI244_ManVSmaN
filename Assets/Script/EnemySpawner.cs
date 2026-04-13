using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {

            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

            yield return new WaitForSeconds(3f);
        }
    }
}