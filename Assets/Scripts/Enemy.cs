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

    void OnTriggerEnter(Collider other)
    {
        //check the collision is from a player bullet
        Debug.Log("trigger entered");
        if (other.gameObject.layer == bulletPrefab.layer)
        {
            Debug.Log("bullet!!");
            Destroy(gameObject);
        }
    }

    void OnDestroy() //later dont do this on destory make some event
    {
        GameManager.instance.removeEnemy(this);
    }

}
