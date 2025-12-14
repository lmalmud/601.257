using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Teddy Starynski
    Date Created: 11/14/25
    Last Edited: 11/24/25
    Manages the logic behind the Win Screen and Lose Screen. Instead of entering a new scene on win or loss, a UI panel pops up over the screen,
    the cursor is made visible, and the timescale is set to 0.
*/

public class activatePanel : MonoBehaviour
{
    public GameObject panel;
    private bool isActive = false;

    // LM: added in an attempt to fix
    [SerializeField] private float pauseEnableDelay = 0.25f;
    private bool canPause = false;

    void Awake()
    {
        panel = this.gameObject;
        // LM: added to stop auto-activating
        // panel.SetActive(true);
        //so that the game manager can grab references to these
        isActive = false;

    }
    
    void Start()
    {
        panel.SetActive(false); // so that they arent visible before they should be

        // LM: commented out
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.None; // LM: added to unlock the cursor at start
        
    }

    public void deactivateThisPanel()
    {
        //call in game manager to get rid of the error when replaying the game that game manager can't get references to win and lose screens
        panel.SetActive(false); 
        isActive = false; // LM: added
    }
    

    public void activateThisPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            isActive = true;

            // LM: added the two lines below because pause menu loads on game start and you cannot leave
            Cursor.lockState = CursorLockMode.None; // unlock the cursor
            Cursor.visible = true; // make the cursor visible
        }
        
    }

    void Update()
    {
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log()
        }
      
    }
}
