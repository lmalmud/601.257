using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/14/25
    Summary: This script is responsible for storing the player's current state. 
                Currently only in reguards to build mode
*/
public class PlayerStateController : MonoBehaviour
{
    
    public enum PlayerState{ViewMode, BuildMode, PlantMode}
    PlayerState currentState = PlayerState.ViewMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setState(PlayerState state)
    {
        currentState = state;
    }

    public PlayerState getState()
    {
        return currentState;
    }
}
