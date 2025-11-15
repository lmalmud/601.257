/*
    Manages global tower variables and states:
    
        contains a reference to all the towers
        ability to kick off events listened to by the towers 
        
        

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerManager : MonoBehaviour
{
    
    public static TowerManager instance;
    public UnityEvent makeRangeVisible;
    public UnityEvent makeRangeInvisible;
    public bool rangeVisible = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated WavesManager, ignoring this one", gameObject);
        }
    }

    void toggleRangeVisibility()
    {
        if (rangeVisible)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
