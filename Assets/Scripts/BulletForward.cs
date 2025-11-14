using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: Launches a bullet forwards until it needs to be autodestroied after a set period of time.
*/

public class BulletForward : MonoBehaviour
{

    public float delayBeforeDestroy; // how long to wait before the bullet automatically dies
    public float speed;

    private AudioSource audioSource; // sound on entry


    void Start()
    {
        // destroy the bullet if it has been long enough
        Destroy(gameObject, delayBeforeDestroy);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // move it forwards
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) // play sound when enemy is hit
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }

}
