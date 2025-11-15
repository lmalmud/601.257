using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePanel : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
        Cursor.visible = false;
        panel = this.gameObject;
    }
    

    public void activateThisPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
