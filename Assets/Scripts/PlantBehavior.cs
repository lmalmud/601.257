using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: Responsible for the plant giving income
*/

public class PlantBehavior : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private int income = 500;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        
    }

    void giveMoney()
    {
        gm.changeMoney(income);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placePlant()
    {
        gm.onDayStart.AddListener(giveMoney);
    }

    void OnDestroy()
    {
        gm.onDayStart.RemoveListener(giveMoney);
    }
}
