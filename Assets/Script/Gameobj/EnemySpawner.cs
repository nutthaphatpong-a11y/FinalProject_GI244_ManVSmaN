using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Wave[] waves;

    public float timeBetweenWaves = 5f;
public TMP_Text waveText;
void Start()
{
    ApplyDifficulty();
    StartCoroutine(SpawnWaves());
}

IEnumerator SpawnWaves()
{
    yield return new WaitForSeconds(3f);

    for (int w = 0; w < waves.Length; w++)
    {
        Wave wave = waves[w];

        // แสดงเลขเวฟ
        if (waveText != null)
        {
            waveText.text = "Wave " + (w + 1);
        }

        List<GameObject> spawnList = new List<GameObject>();

        // รวมศัตรูทั้งหมดของเวฟ
        for (int i = 0; i < wave.enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < wave.counts[i]; j++)
            {
                spawnList.Add(wave.enemyPrefabs[i]);
            }
        }

        // สุ่มลำดับ
        for (int i = 0; i < spawnList.Count; i++)
        {
            int rand = Random.Range(0, spawnList.Count);

            GameObject temp = spawnList[i];
            spawnList[i] = spawnList[rand];
            spawnList[rand] = temp;
        }

        // spawn ทีละตัว
        foreach (GameObject enemy in spawnList)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemyObj = Instantiate(enemy, spawn.position, Quaternion.identity);

            Enemy e = enemyObj.GetComponent<Enemy>();

            if (e != null)
            {
                e.maxHP *= GameSettings.enemyHPMultiplier;
                e.speed *= GameSettings.enemySpeedMultiplier;
                e.currentHP = e.maxHP;
            }

            yield return new WaitForSeconds(wave.spawnDelay);
        }

        // รอให้ศัตรูตายหมดก่อนขึ้นเวฟใหม่
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }

    if (waveText != null)
    {
        waveText.text = "All Waves Clear!";
    }

    Debug.Log("All waves finished!");
}

void ApplyDifficulty()
{
    switch (GameSettings.difficulty)
    {
        case 0: // Easy
            timeBetweenWaves = 6f;
            break;

        case 1: // Medium
            timeBetweenWaves = 4f;
            break;

        case 2: // Hard
            timeBetweenWaves = 2f;
            break;
    }
}
}