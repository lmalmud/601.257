using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: This script is responsible for keeping track of the player's build mode and 
				the actual placing of towers/plants
*/

public class PlayerActionHandler : MonoBehaviour
{
    
    [Header("Player look location")]
    [SerializeField] private float distance = 10;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private GameObject wandEndPoint;
    [SerializeField] private GameObject flashLight;

    [Header("Tower/plant placement")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private PlayerStateController playerState;
    




    private Camera mainCam;
    private Vector3 lookLocation;
    private Vector3 targetPlaceLocation;
    private GameObject preview;
    private GameManager gm;

    private bool lightOn = true;
    
    
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
        targetPlaceLocation = lookLocation;
        // Bounds bounds = preview.GetComponent<BoxCollider>().bounds;
        // targetPlaceLocation.y += bounds.extents.y;
        // Vector3 towerPlaceTarget = lookLocation;
        // Debug.Log("pre: " + towerPlaceTarget);
        // towerPlaceTarget.y += towerYoffset;
        // Debug.Log("post: " + towerPlaceTarget.y);
    }

    public void OnToggleFlashlight(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if (playerState.getState() != PlayerStateController.PlayerState.ViewMode) return;
        
        setFlashLight(!lightOn);
    }

    void setFlashLight(bool status)
    {

        lightOn = status;
        if (status)
        {
            flashLight.GetComponent<Light>().intensity = 60;
        }
        else
        {
            flashLight.GetComponent<Light>().intensity = 0;
        }
    }


    public void OnViewMode(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if (playerState.getState() != PlayerStateController.PlayerState.ViewMode)
        {
            setFlashLight(true);
            Destroy(preview);
            playerState.setState(PlayerStateController.PlayerState.ViewMode);
        }
    }

    public void OnBuildMode(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if (playerState.getState() == PlayerStateController.PlayerState.ViewMode)
        {
            setFlashLight(false);
            setPreview(towerPrefab);
            preview.GetComponent<TowerAttack>().setIsPlaced(false);
            playerState.setState(PlayerStateController.PlayerState.BuildMode);
        }
        else if (playerState.getState() == PlayerStateController.PlayerState.PlantMode)
        {
            Destroy(preview);
            setPreview(towerPrefab);
            preview.GetComponent<TowerAttack>().setIsPlaced(false);
            playerState.setState(PlayerStateController.PlayerState.BuildMode);
        }
    }

    public void OnPlantMode(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if (playerState.getState() == PlayerStateController.PlayerState.ViewMode)
        {
            setFlashLight(false);
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
		preview.GetComponent<Collider>().isTrigger = true;
        preview.GetComponent<PlaceableMaterialManager>().setMaterial(0);
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
        int cost = preview.GetComponent<TowerCost>().getPrice();
        if(!gm.spendMoney(cost)) return false;
        //TODO: check for valid tower placement
        return true;
    }
    
    
    
    public void OnPlace(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if (preview == null) return;
        if (!placementValid())
        {
            //TODO: add some visual feedback -> make preview red?
            Debug.Log("can't place this tower");
            return;
        }
        //TODO: change this to co routine w/ LERPs
        PlaceableMaterialManager towerPlaceEffects = preview.GetComponent<PlaceableMaterialManager>();
        towerPlaceEffects.setMaterial(1);
        towerPlaceEffects.playPlaceParticle();
        setOpacity(preview, 1);
		preview.GetComponent<Collider>().isTrigger = false;
		if(playerState.getState() == PlayerStateController.PlayerState.BuildMode)
        {
            preview.GetComponent<TowerAttack>().setIsPlaced(true);
            setPreview(towerPrefab);
        }
		else if(playerState.getState() == PlayerStateController.PlayerState.PlantMode)
        {
            preview.GetComponent<PlantBehavior>().placePlant();
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

