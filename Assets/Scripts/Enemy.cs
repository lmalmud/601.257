using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

/*
    Author: Teddy Starynski
    Date Created: 9/27/25
    Last Edited: 11/24/25
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
    
    private Coroutine fireDoTCoroutine; // a function that will execute over multiple frames

    void Awake()
    {
        fsm = GetComponentInChildren<EnemyFSM>();
        life = GetComponent<Life>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Health", life.amount);

        var endPointDetector = GameObject.Find("EndPoint").GetComponent<EndPointDetection>();
        // endPointDetector.onReachEnd.AddListener(endPointAnim);
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

                // start damage over time for fire bullets
                if (bd.hasDOT) // a new property in the BulletDamage script
                {
                    if (fireDoTCoroutine != null) 
                    {
                        StopCoroutine(fireDoTCoroutine); // stop the old one if one is already running
                    }
                    fireDoTCoroutine = StartCoroutine(FireDamageOverTime(bd.dotDamage, bd.dotDuration, bd.dotInterval));
                }
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

    // the coroutine for DoT
    private IEnumerator FireDamageOverTime(int dotDamage, float duration, float interval)
    {
        float elapsedTime = 0f;
        
        // keep going until time runs out or enemy dies
        while (elapsedTime < duration && life.amount > 0)
        {
            yield return new WaitForSeconds(interval); // between damage ticks (a property of bullet)
            
            // apply the damage and update UI
            life.amount -= dotDamage;
            animator.SetFloat("Health", life.amount);
            animator.SetTrigger("IsHit");
            
            elapsedTime += interval; // update how much time passed
        }
        
        fireDoTCoroutine = null; // do not refer to the coroutine once it ended
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

