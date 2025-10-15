using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/1/25
    Date Last Updated: 10/10/25
    Summary: Tower attack mechanism- tower identifies closest enemy, aims, and shoots.
*/

public class TowerAttack : MonoBehaviour
{

    public GameObject bulletPrefab; // the bullet prefab
    public GameObject shootPoint; // the place at which bullets will be released from
    public Sight sightSensor; // the gameobject that will handle sight
    public float shootRate; // how frequently this tower can shoot - measured in bullets/second
    private float lastFired; // the last time that a bullet was fired
    public bool isPlaced = true;

    private float projectileSpeed = 20f; // speed to use if we can't get speed from bullet object

    void Update()
    {
        if (!isPlaced) return;
        // reference for auto-shooting: https://discussions.unity.com/t/automatic-shooting-script/112483
        // if it is within the time frame to shoot and there is actually an object in range
        if (((Time.time - lastFired) > 1 / shootRate) & sightSensor.detectedObject != null)
        {
            GameObject clone = Instantiate(bulletPrefab);

            // access the .transform property of the detected object and then its position
            Vector3 targetCenter = sightSensor.detectedObject.GetComponent<Collider>().bounds.center;
            Vector3 targetVelocity = sightSensor.detectedObject.GetComponent<Rigidbody>().velocity;

            Vector3 spawnPoint = shootPoint.transform.position;

            // need the speed of the bullet for calculations - grab it from prefab
            BulletForward prefabBullet = bulletPrefab.GetComponent<BulletForward>();
            float bulletSpeedToUse = prefabBullet != null ? prefabBullet.speed : projectileSpeed;

            // if we decide we want to spawn at the same height as the enemy...
            // new Vector3(shootPoint.transform.position.x, targetCenter.y, shootPoint.transform.position.z);

            // want the vector to the center of the object, not the pivot
            // FIXME: do we want to aim to take into account the rate at which the enemies are moving?
            Vector3 directionToObject = targetCenter - spawnPoint;

            float distance = directionToObject.magnitude; // need in order to approximate hit time (d = rt)
            
            float timeToHit;
            if (projectileSpeed > 0f) // don't want to divide by zero
            {
                timeToHit = distance / bulletSpeedToUse;
            }
            else
            {
                timeToHit = 0f;
            }

            Vector3 predictedPos = targetCenter + targetVelocity * timeToHit;
            Vector3 aimDir = predictedPos - spawnPoint;

            // uncomment to see line to where bullets aim
            Debug.DrawLine(shootPoint.transform.position, targetCenter, Color.blue, 2f);

            clone.transform.position = spawnPoint; // make the bullet spawn at the shootPoint of the tower
            clone.transform.rotation = Quaternion.LookRotation(aimDir); // make the bullet point towards the enemy

            // NOTE: if this ends up not working... switch aimDir back to directionToObject and that's it :)

            lastFired = Time.time; // update the time that was last fired to be right now
        }

    }

    public void setIsPlaced(bool value)
    {
        isPlaced = value;
    }
}
