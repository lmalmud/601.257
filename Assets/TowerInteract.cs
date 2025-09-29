using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteract : MonoBehaviour
{
    
    public float distance = 3;

    public float angle = 60;
    public LayerMask objectsLayer;
    public LayerMask obstaclesLayer;
    public Collider detectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnPlace()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayer);
        detectedObject = null;
        Debug.Log(colliders.Length);
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider currCollider = colliders[i];
            // Debug.DrawLine(transform.position, currCollider.bounds.center, Color.green);
            if (!currCollider.gameObject.CompareTag("TowerBase"))
            {
                continue;
            }
            Vector3 directionToController = Vector3.Normalize(currCollider.bounds.center - transform.position);
            
            float angleToCollider = Vector3.Angle(transform.forward, directionToController);
            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, currCollider.bounds.center, out RaycastHit hit, obstaclesLayer))
                {
                    detectedObject = currCollider;
                    break;
                }

            }
        }

        if (detectedObject == null)
        {
            return;
        }
        
        detectedObject.gameObject.GetComponent<ControlTower>().PlaceTower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
        
        Vector3 rightDirection = Quaternion.Euler(0,angle,0) *  transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection*distance);
        
        Vector3 leftDirection = Quaternion.Euler(0,-angle,0) *  transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection*distance);
    }
}
