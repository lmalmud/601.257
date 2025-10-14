using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    
    public float distance = 10;

    public LayerMask terrainLayer;


    private Camera mainCam;
    public GameObject towerPrefab;
    public GameObject plantPrefab;

    public Vector3 lookLocation;
    public Vector3 targetPlaceLocation;
    public GameObject wandEndPoint;
    private GameObject preview;
    public PlayerStateController playerState;
    private GameManager gm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        gm = GameManager.instance;
        if (gm == null)
        {
            Debug.Log("TowerInteract::Start(): GameManager is null");
        }
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

    void getBuildLocation()
    {
        getLookLocation();
        Bounds bounds = preview.GetComponent<BoxCollider>().bounds;
        targetPlaceLocation = lookLocation;
        //targetPlaceLocation.y += bounds.extents.y;
        // Vector3 towerPlaceTarget = lookLocation;
        // Debug.Log("pre: " + towerPlaceTarget);
        // towerPlaceTarget.y += towerYoffset;
        // Debug.Log("post: " + towerPlaceTarget.y);
    }


    void OnViewMode()
    {
        if (playerState.getState() != PlayerStateController.PlayerState.ViewMode)
        {
            Destroy(preview);
            playerState.setState(PlayerStateController.PlayerState.ViewMode);
        }
    }

    void OnBuildMode()
    {
        if (playerState.getState() == PlayerStateController.PlayerState.ViewMode)
        {
            setPreview(towerPrefab);
            playerState.setState(PlayerStateController.PlayerState.BuildMode);
        }
        else if (playerState.getState() == PlayerStateController.PlayerState.PlantMode)
        {
            Destroy(preview);
            setPreview(towerPrefab);
            playerState.setState(PlayerStateController.PlayerState.BuildMode);
        }
    }

    void OnPlantMode()
    {
        if (playerState.getState() == PlayerStateController.PlayerState.ViewMode)
        {
            setPreview(plantPrefab);
            playerState.setState(PlayerStateController.PlayerState.PlantMode);
        }
        else if (playerState.getState() == PlayerStateController.PlayerState.BuildMode)
        {
            Destroy(preview);
            setPreview(plantPrefab);
            playerState.setState(PlayerStateController.PlayerState.PlantMode);
        }
    }

    void setPreview(GameObject prefab)
    {
        preview = Instantiate(prefab, targetPlaceLocation, Quaternion.identity);
        setOpacity(preview, .5f);
    }

    void setOpacity(GameObject item, float opacity)
    {
        Color itemColor = item.GetComponent<Renderer>().material.color;
        itemColor.a = opacity;
        item.GetComponent<Renderer>().material.color = itemColor;
    }

    bool placementValid()
    {
        int cost = preview.GetComponent<TowerInfo>().getPrice();
        if(!gm.spendMoney(cost)) return false;
        //TODO: check for valid tower placement
        return true;
    }
    
    
    
    void OnPlace()
    {
        if (preview == null) return;
        if (!placementValid())
        {
            //TODO: add some visual feedback -> make preview red?
            Debug.Log("can't place this tower");
            return;
        }
        //TODO: change this to co routine w/ LERPs
        setOpacity(preview, 1);
		if(playerState.getState() == PlayerStateController.PlayerState.BuildMode)
        {
            setPreview(towerPrefab);
        }
		else if(playerState.getState() == PlayerStateController.PlayerState.PlantMode)
        {
            setPreview(plantPrefab);
        }
        //preview = Instantiate(towerPrefab, targetPlaceLocation, Quaternion.identity);
        //setOpacity(preview, .5f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (preview != null)
        {
			getBuildLocation();
            preview.transform.position = targetPlaceLocation;
        }
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
        
    }
}

