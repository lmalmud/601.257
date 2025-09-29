using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;


    void Start()
    {
        GameManager.instance.addEnemy(this);
    }

    void onTriggerEnter(Collider other)
    {
        //check the collision is from a player bullet
        if (other.gameObject.layer == bulletPrefab.layer)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GameManager.instance.removeEnemy(this);
    }

}
