using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Author: Teddy Starynski
    Date Created: 11/13/25
    Last Edited: 11/13/25
    Controls the UI which shows the player how much health they have.

*/

public class HealthUI : MonoBehaviour
{
    
    Image image;
    public Sprite sprite;

    [SerializeField] private Sprite[] spritesList;

    void Start()
    {
        image = GetComponent<Image>();

        GameManager.instance.onHealthChange.AddListener(changeHealthUI);
    }

    void changeHealthUI()
    {
        image.sprite = spritesList[GameManager.instance.getLife()];
    }
}
