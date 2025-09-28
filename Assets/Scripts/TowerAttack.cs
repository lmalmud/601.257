using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject shootPoint;
    public Sight sightSensor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sightSensor.detectedObject;
        GameObject clone = Instantiate(bulletPrefab);
        clone.transform.position = shootPoint.transform.position;
        //clone.transform.rotation = // the angle to the position of the detected object;
    }
}
