using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Detects collisions of bullets with the enemy instance. 
    Adds and removes enemy instances from the game manager.
*/

public class Enemy : MonoBehaviour
{

    [Header("Enemy Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Life life;
    [SerializeField] private ParticleSystem fireParticlePrefab;

    private EnemyFSM fsm;


    void Awake()
    {
        fsm = GetComponentInChildren<EnemyFSM>();
        life = GetComponent<Life>();

    }

    void Start()
    {
        GameManager.instance.addEnemy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        //check the collision is from a player bullet
        if (other.gameObject.layer == bulletPrefab.layer)
        {
            // ADDED BY LUCY 10/30
            // try to read damage from the bullet; fall back to 1 if component missing
            BulletDamage bd = other.gameObject.GetComponent<BulletDamage>();
            int damage = (bd != null) ? bd.damage : 1;

            life.amount -= damage; // CHANGED BY LUCY 10/30
            Destroy(other.gameObject); // destroy the bullet on hit

            if (bd != null && bd.type == "fire" && fireParticlePrefab != null)
            {
                Instantiate(fireParticlePrefab, transform.position, Quaternion.identity, transform);
            }

            if (bd != null && bd.type == "water")
            {
                // apply water slow-down effect here @teddy I don't know how to do this
            }

            // if (life.amount <= 0)
            // {
            //     Destroy(gameObject);
            // }
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            fsm.updateCheckpoint();
        }
    }

    void OnDestroy() //later dont do this on destory make some event
    {
        GameManager.instance.removeEnemy(this);
    }

}

