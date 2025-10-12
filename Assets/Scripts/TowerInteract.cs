using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteract : MonoBehaviour
{
    
    public float distance = 3;

    public float angle = 60;
    public LayerMask terrainLayer;
    private Camera mainCam;
    public GameObject towerPrefab;

    public Vector3 lookLocation;
    public Vector3 targetPlaceLocation;
    public GameObject wandEndPoint;
    private GameObject towerPreview;
    public PlayerStateController playerState;
    private float towerYoffset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }
    void getLookLocation()
    {
        Vector3 gaze = mainCam.transform.forward;
        RaycastHit hitInfo;
        if (Physics.Raycast(wandEndPoint.transform.position, gaze, out hitInfo, distance, terrainLayer))
        {
            lookLocation = hitInfo.point;
        }
        else
        {
            lookLocation = wandEndPoint.transform.position + distance * gaze;
        }
        // Gizmos.color = Color.magenta;
        // Gizmos.DrawRay(wandEndPoint.transform.position, gaze*10);
    }

    //precondition: towerPrefab is set to the tower type that wants to be displayed/placed
    void getBuildLocation()
    {
        getLookLocation();
        Bounds towerBounds = towerPrefab.GetComponent<MeshRenderer>().bounds;
        towerYoffset = towerBounds.extents.y;
        targetPlaceLocation = lookLocation;
        targetPlaceLocation.y += towerYoffset;
        // Vector3 towerPlaceTarget = lookLocation;
        // Debug.Log("pre: " + towerPlaceTarget);
        // towerPlaceTarget.y += towerYoffset;
        // Debug.Log("post: " + towerPlaceTarget.y);
    }


    void OnViewMode()
    {
        if (playerState.getState() == PlayerStateController.PlayerState.BuildMode)
        {
            Destroy(towerPreview);
            playerState.setState(PlayerStateController.PlayerState.ViewMode);
        }
    }

    void OnBuildMode()
    {
        if (playerState.getState() == PlayerStateController.PlayerState.ViewMode)
        {
            
            towerPreview = Instantiate(towerPrefab, targetPlaceLocation, Quaternion.identity);
            // Bounds towerBounds = towerPreview.GetComponent<MeshRenderer>().bounds;
            // towerYoffset = towerBounds.extents.y;
            // Vector3 currPos = towerPreview.transform.position;
            // currPos.y += towerYoffset;
            // towerPreview.transform.position = currPos;
            setOpacity(towerPreview, .5f);
            playerState.setState(PlayerStateController.PlayerState.BuildMode);
            
        }
        // if (towerPreview == null)
        // {
        //     towerPreview = Instantiate(towerPrefab, targetPos, Quaternion.identity);
        //     setOpacity(towerPreview, .5f);
        // }
        
        
    }

    void setOpacity(GameObject item, float opacity)
    {
        Color itemColor = item.GetComponent<Renderer>().material.color;
        itemColor.a = opacity;
        item.GetComponent<Renderer>().material.color = itemColor;
    }
    
    
    
    void OnPlace()
    {
        if (towerPreview == null) return;
        setOpacity(towerPreview, 1);
        towerPreview = Instantiate(towerPrefab, targetPlaceLocation, Quaternion.identity);
        setOpacity(towerPreview, .5f);

        // return;
        // Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayer);
        // detectedObject = null;
        // //Debug.Log(colliders.Length);
        // for (int i = 0; i < colliders.Length; i++)
        // {
        //     Collider currCollider = colliders[i];
        //     // Debug.DrawLine(transform.position, currCollider.bounds.center, Color.green);
        //     if (!currCollider.gameObject.CompareTag("TowerBase"))
        //     {
        //         continue;
        //     }
        //     Vector3 directionToController = Vector3.Normalize(currCollider.bounds.center - transform.position);
        //     
        //     float angleToCollider = Vector3.Angle(transform.forward, directionToController);
        //     if (angleToCollider < angle)
        //     {
        //         if (!Physics.Linecast(transform.position, currCollider.bounds.center, out RaycastHit hit, obstaclesLayer))
        //         {
        //             detectedObject = currCollider;
        //             break;
        //         }
        //
        //     }
        // }
        //
        // if (detectedObject == null)
        // {
        //     return;
        // }
        //
        // detectedObject.gameObject.GetComponent<ControlTower>().PlaceTower();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        getBuildLocation();
        
        if (towerPreview != null)
        {
            towerPreview.transform.position = targetPlaceLocation;
        }
        
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

