using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{

    public GameObject bulletPrefab; // the bullet prefab
    public GameObject shootPoint; // the place at which bullets will be released from
    public Sight sightSensor; // the gameobject that will handle sight
    public float shootRate; // how frequently this tower can shoot

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject clone = Instantiate(bulletPrefab);

        // access the .transform property of the detected object and then its position
        Vector3 directionToObject = sightSensor.detectedObject.transform.position - transform.position;
        
        clone.transform.position = shootPoint.transform.position;
    }
}
