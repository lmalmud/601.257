using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: Detects an object that is in the objectsLayer, ignoring entities in the obstaclesLayer.
*/

public class Sight : MonoBehaviour
{
    public Collider detectedObject; // the list of objects that are in sight
    public float distance; // the maximum range that objects will be able to be detected
    public float angle; // the angle of vision to see objects
    public LayerMask objectsLayers; // the layer for things we can hit
    public LayerMask obstaclesLayers; // the layer for things to avoid

    // Reference point (e.g., prefab instance) to measure closeness to.
    // If null, falls back to this.transform.
    public Transform referencePoint;


    void Update()
    {
        // returns an array of colliders found within the sphere
        // note the first parameter is the position of the AI object
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayers);

        detectedObject = null;

        // what is the object that is closest to the reference point?
        float bestDist = Mathf.Infinity;
        Collider bestCollider = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];

            Vector3 directionToController = Vector3.Normalize(collider.bounds.center - transform.position);

            float angleToCollider = Vector3.Angle(transform.forward, directionToController);


            if (angleToCollider < angle)
            {

                // if the line does *not* hit an obstacle
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstaclesLayers))
                {

                    //Debug.DrawLine(transform.position, collider.bounds.center, Color.green);

                    // compute distance to the configured reference point (or this.transform if null)
                    Vector3 refPos = (referencePoint != null) ? referencePoint.position : transform.position;
                    float distToRef = Vector3.Distance(refPos, collider.bounds.center);

                    if (distToRef < bestDist)
                    {
                        bestDist = distToRef;
                        bestCollider = collider;
                    }
                }

                // if the line hits an obstacle, draw a line to it
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }

            detectedObject = bestCollider;

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);

        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
