using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Brady Bock
    Depreciated due to a new tower placement system
*/

public class ControlTower : MonoBehaviour
{
    
    public GameObject towerPrefab;

    public GameObject spawnLocation;
    
    bool hasTower = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower()
    {
        if (hasTower)
        {
            return;
        }
        Instantiate(towerPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        hasTower = true;
    }
}
