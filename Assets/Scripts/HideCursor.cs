using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 10/31/23
    Date Last Updated: 10/31/23
    Summary: Eliminate what my friends and I call "the mouse thing" (the bane of our existence when playing Minecraft)
*/

public class HideCursor : MonoBehaviour
{
    void Start()
    {
        // Hide the mouse cursor
        Cursor.visible = false;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Optional: press Escape to unlock and show the cursor again
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}