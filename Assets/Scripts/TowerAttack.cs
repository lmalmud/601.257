using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{

    public GameObject bulletPrefab; // the bullet prefab
    public GameObject shootPoint; // the place at which bullets will be released from
    public Sight sightSensor; // the gameobject that will handle sight
    public float shootRate; // how frequently this tower can shoot - measured in bullets/second
    private float lastFired; // the last time that a bullet was fired

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // reference for auto-shooting: https://discussions.unity.com/t/automatic-shooting-script/112483
        if ((Time.time - lastFired) > 1 / shootRate)
        {
            GameObject clone = Instantiate(bulletPrefab);

            // access the .transform property of the detected object and then its position
            Vector3 directionToObject = sightSensor.detectedObject.transform.position - transform.position;

            //Debug.DrawLine(transform.position, sightSensor.detectedObject.transform.position, Color.red);

            clone.transform.position = shootPoint.transform.position; // make the bullet spawn at the shootPoint of the tower
            clone.transform.rotation = Quaternion.LookRotation(directionToObject); // make the bullet point towards the enemy

            lastFired = Time.time; // update the time that was last fired to be right now
        }
        
    }
}
