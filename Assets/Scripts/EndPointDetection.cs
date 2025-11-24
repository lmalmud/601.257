using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Author: Teddy Starynski
    Date Created: 9/27/25
    Last Edited: 9/27/25
    Detects when enemies have reached the house, and sends out an event message when that happens.

*/

public class EndPointDetection : MonoBehaviour
{
    public UnityEvent onReachEnd;

    void OnTriggerEnter(Collider other)
    {
        //for now will have enemies disappear and lose one life once the endpoint is reached
        if (this.gameObject.tag == "EndPoint")
        {
            onReachEnd.Invoke();

            // Destroy(other.gameObject);
        }

        
    }

}
