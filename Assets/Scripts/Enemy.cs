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
            
            life.amount -= 25;
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
