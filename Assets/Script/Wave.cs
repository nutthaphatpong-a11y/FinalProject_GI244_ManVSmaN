using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs; // ประเภทศัตรู
    public int[] counts;              // จำนวนของแต่ละประเภท

    public float spawnDelay = 1f;
}