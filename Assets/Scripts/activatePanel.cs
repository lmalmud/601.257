using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePanel : MonoBehaviour
{
    public GameObject panel;
    
    void Awake()
    {
        panel.SetActive(false);
    }
    

    public void activateThisPanel()
    {
        
        panel.SetActive(true);
        Time.timeScale = 0;
    }
}
