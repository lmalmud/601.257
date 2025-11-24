using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Author: Teddy Starynski
    Date Created: 10/5/25
    Last Edited: 10/5/25
    Controls the life total of the game object with this script and invokes an event when the life total reaches 0, then destroys the object.

*/

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
