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

    void Awake()
    {
        panel = this.gameObject;
        panel.SetActive(true);
        //so that the game manager can grab references to these
        isActive = false;

    }
    
    void Start()
    {
        //panel.SetActive(false); //so that they arent visible before they should be
        Cursor.visible = false;
        
    }

    public void deactivateThisPanel()
    {
        //call in game manager to get rid of the error when replaying the game that game manager can't get references to win and lose screens
        panel.SetActive(false); 
    }
    

    public void activateThisPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            isActive = true;
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
