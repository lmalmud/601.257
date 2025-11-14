using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public GameObject losePanel;
    
    
    void Awake()
    {
        losePanel.SetActive(false);
    }
    

    public void activateLosePanel()
    {
        Debug.Log("activate lose panel");
        
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
