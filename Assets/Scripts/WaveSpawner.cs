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

    //array of points along the path in the order the enemy should pass them
    //the last entry in this array should be the base
    [SerializeField] private Transform[] checkpoints; 

    void Start()
    {
        GameManager.instance.addWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    void Spawn()
    {
        GameObject eObj = Instantiate(enemyPrefab, transform.position, transform.rotation);
        EnemyFSM e = eObj.GetComponentInChildren<EnemyFSM>();
        e.setSpawner(this);
    }

    void EndSpawner()
    {
        GameManager.instance.removeWave(this);
        CancelInvoke();
    }

    //returns the enxt chackpoint along the path given the index the enemy just passed
    public Transform getNextCheckpoint(int index)
    {
        return checkpoints[index + 1];
    }
}
