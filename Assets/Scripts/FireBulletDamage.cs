using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/30/2025
    Date Last Updated: 10/30/2025
    Summary: Will set enemies on fire.
*/


public class FireBulletDamage : MonoBehaviour, IApplyEffect
{
    [Header("Fire Effect")]
    public float duration = 3f;
    public float damagePerSecond = 1f;
    public int initialDamage = 0; // fire doesn't initally do damage

    public void ApplyTo(Enemy enemy)
    {
        if (initialDamage != 0)
        {
            enemy.TakeDamage(initialDamage);
        }
        enemy.AddStatus("fire", duration, damagePerSecond);
    }

}
