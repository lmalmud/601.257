using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
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
