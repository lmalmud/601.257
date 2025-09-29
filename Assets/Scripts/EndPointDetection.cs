using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPointDetection : MonoBehaviour
{
    public UnityEvent onReachEnd;

    void OnTriggerEnter(Collider other)
    {
        //edited collision matrix so this should only care about enemies
        //print("oh no!");

        //for now will have enemies disappear and lose one life once the endpoint is reached
        onReachEnd.Invoke();

        Destroy(other.gameObject);
    }

}
