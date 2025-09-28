using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour
{

    public float delayBeforeDestroy; // how long to wait before the bullet automatically dies
    public float speed;


    void Start()
    {
        // destroy the bullet if it has been long enough
        Destroy(gameObject, delayBeforeDestroy);
    }

    void Update()
    {
        // move it forwards
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
