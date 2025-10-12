using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    private GameManager gm;

    public int income = 500;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.onDayStart.AddListener(giveMoney);
    }

    void giveMoney()
    {
        gm.changeMoney(income);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        gm.onDayStart.RemoveListener(giveMoney);
    }
}
