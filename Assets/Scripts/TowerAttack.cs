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

    // Update is called once per frame
    void Update()
    {
        // reference for auto-shooting: https://discussions.unity.com/t/automatic-shooting-script/112483
        // if it is within the time frame to shoot and there is actually an object in range
        if (((Time.time - lastFired) > 1 / shootRate) & sightSensor.detectedObject != null)
        {
            GameObject clone = Instantiate(bulletPrefab);

            // access the .transform property of the detected object and then its position
            Vector3 targetCenter = sightSensor.detectedObject.GetComponent<Collider>().bounds.center;
            Vector3 targetVelocity = sightSensor.detectedObject.GetComponent<Rigidbody>().velocity;

            Vector3 spawnPoint = shootPoint.transform.position;

            // if we decide we want to spawn at the same height as the enemy...
            // new Vector3(shootPoint.transform.position.x, targetCenter.y, shootPoint.transform.position.z);

            // want the vector to the center of the object, not the pivot
            // FIXME: do we want to aim to take into account the rate at which the enemies are moving?
            Vector3 directionToObject = targetCenter - spawnPoint;

            // uncomment to see line to where bullets aim
            //Debug.DrawLine(shootPoint.transform.position, targetCenter, Color.blue, 2f);

            clone.transform.position = spawnPoint; // make the bullet spawn at the shootPoint of the tower
            clone.transform.rotation = Quaternion.LookRotation(directionToObject); // make the bullet point towards the enemy

            lastFired = Time.time; // update the time that was last fired to be right now
        }
        
    }
}
