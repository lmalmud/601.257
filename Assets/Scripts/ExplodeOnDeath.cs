using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Teddy Starynski
    Date Created: 10/17/25
    Last Edited: 10/17/25
    Creates an explosion when onDeath event detected
*/
public class ExplodeOnDeath : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;

    void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(PlayParticle);
    }

    void PlayParticle()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }
}
