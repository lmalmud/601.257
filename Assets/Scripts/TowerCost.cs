using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/10/25
    Date Last Updated: 10/10/25
    Summary: Used to identify how much a tower costs to be placed.
*/

public class TowerCost : MonoBehaviour
{

    [SerializeField]
    private int towerCost;
    
    
    public int getPrice()
    {
        return towerCost;
    }
}
