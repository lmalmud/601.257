using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Spawns in a wave of enemies based on the given start time, end time, and spawn rate.
*/

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Spawner Parameters")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float startTime = 5;
    [SerializeField] private float endTime = 10;
    [SerializeField] private float spawnRate = 2;

    void Start()
    {
        GameManager.instance.addWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    void EndSpawner()
    {
        GameManager.instance.removeWave(this);
        CancelInvoke();
    }
}
