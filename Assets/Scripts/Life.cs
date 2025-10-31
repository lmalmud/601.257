using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] public float amount = 50;
    public UnityEvent onDeath;

    void Update()
    {

        if (amount <= 0)
        {
            onDeath.Invoke(); //sends out the onDeath event message
            Destroy(gameObject);
        }
    }
}
