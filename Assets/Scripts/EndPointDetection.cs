using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
