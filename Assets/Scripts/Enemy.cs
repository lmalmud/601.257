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
    [SerializeField] private int life = 5;


    void Start()
    {
        GameManager.instance.addEnemy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        //check the collision is from a player bullet
        if (other.gameObject.layer == bulletPrefab.layer)
        {
            //Debug.Log("bullet!!");
            life--;

            if(life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy() //later dont do this on destory make some event
    {
        GameManager.instance.removeEnemy(this);
    }

}
