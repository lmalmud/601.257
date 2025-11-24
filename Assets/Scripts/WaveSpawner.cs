using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Teddy Starynski
    Date Created: 9/27/25
    Last Edited:  9/27/25
    Spawns in a wave of enemies based on the given start time, end time, and spawn rate.
*/

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Spawner Parameters")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float startTime = 0;
    [SerializeField] private float endTime = 20;
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private int numWaves = 2;

    //array of points along the path in the order the enemy should pass them
    //the last entry in this array should be the base
    [SerializeField] private Transform[] checkpoints; 
    
    private int waveCounter = 0;
    
    void Start()
    {
        GameManager.instance.addWave(this);
        GameManager.instance.addWave(this);
        // InvokeRepeating("Spawn", startTime, spawnRate);
        // Invoke("EndSpawner", endTime);
        GameManager.instance.onNightStart.AddListener(startWave);
    }

    void startWave()
    {
        // GameManager.instance.addWave(this);
        if (waveCounter < numWaves)
        {
            InvokeRepeating("Spawn", startTime, spawnRate);
            Invoke("EndSpawner", endTime);
            waveCounter++;
        }
        
    }

    void Spawn()
    {
        int randIndex = Random.Range(0, 2); //pick a random enemy from prefabs list
        GameObject eObj = Instantiate(enemyPrefabs[randIndex], transform.position, transform.rotation);
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
