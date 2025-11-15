using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
    private Animator animator;
    


    void Awake()
    {
        fsm = GetComponentInChildren<EnemyFSM>();
        life = GetComponent<Life>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Health", life.amount);

        var endPointDetector = GameObject.Find("EndPoint").GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.AddListener(endPointAnim);
        life.onDeath.AddListener(death);
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
            animator.SetFloat("Health", life.amount);

            animator.SetTrigger("IsHit");

            Destroy(other.gameObject); // destroy the bullet on hit

            if (bd != null && bd.type == "fire" && fireParticlePrefab != null)
            {
                Instantiate(fireParticlePrefab, transform.position, Quaternion.identity, transform);
            }

            if (bd != null && bd.type == "water")
            {
                this.GetComponent<NavMeshAgent>().speed = (float)this.GetComponent<NavMeshAgent>().speed / 2;
            }

        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            fsm.updateCheckpoint();
        }
    }

    void endPointAnim()
    {
        animator.SetBool("IsAtEndpoint", true); 
    }

    void death()
    {
        GameManager.instance.removeEnemy(this);
    }

}

