using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Changes the scene to the game or to the controls depending on which was clicked
*/

public class ChangeToGame : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level3_copy");
    }

    public void ToControls()
    {
        SceneManager.LoadScene("Controls");
    }
}
