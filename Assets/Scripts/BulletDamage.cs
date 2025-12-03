using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/30/2025
    Date Last Updated: 11/27/2025
    Summary: Manages the damage properties the bullet is capable of.
*/

public class BulletDamage : MonoBehaviour
{
    public int damage = 3;
    public string type = "basic";

    [Header("Damage Over Time")]
    public bool hasDOT = false;
    public int dotDamage = 1;
    public float dotDuration = 3f;
    public float dotInterval = 0.5f;
}
