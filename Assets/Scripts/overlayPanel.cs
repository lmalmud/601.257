using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Brady Bock
    Date Created: 12/14/25
    Last Edited: 12/14/25
    Manages the logic behind an overlay panel that does not pause the game.
    modeled after activatePanel.cs
*/

public class overlayPanel : MonoBehaviour
{
    public GameObject panel;

    void Awake()
    {
        panel = this.gameObject;

    }
    
    void Start()
    {
        panel.SetActive(false);
    }

    public void deactivateThisPanel()
    {
        panel.SetActive(false); 
    }
    public void activateThisPanel()
    {
        panel.SetActive(true); 
    }
    

    public void toggleThisPanel()
    {
        if (panel != null)
        {
            if(panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked; // optional for FPS controls
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true; // make the cursor visible
            }
            panel.SetActive(!panel.activeSelf);
        }
        
    }

    void Update()
    {

      
    }
}
