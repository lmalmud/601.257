using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
