using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
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
