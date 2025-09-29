using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public Collider detectedObject; // the list of objects that are in sight
    public float distance; // the maximum range that objects will be able to be detected
    public float angle; // the angle of vision to see objects
    public LayerMask objectsLayers; // the layer for things we can hit
    public LayerMask obstaclesLayers; // the layer for things to avoid

    // Update is called once per frame
    void Update()
    {
        // returns an array of colliders found within the sphere
        // note the first parameter is the position of the AI object
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayers);

        detectedObject = null;

        print(colliders.Length);

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
                    detectedObject = collider;
                    break;
                }

                // if the line hits an obstacle, draw a line to it
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }

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
