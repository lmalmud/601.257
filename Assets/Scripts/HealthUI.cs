using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Author: Teddy Starynski
    Date Created: 11/13/25
    Last Edited: 12/14/25
    Controls the UI which shows the player how much health they have.
    Updated by Lucy to only update the screen when something is edited.
*/

public class HealthUI : MonoBehaviour
{
    
    Image image;
    public Sprite sprite;

    [SerializeField] private Sprite[] spritesList;

    void Start()
    {
        image = GetComponent<Image>();
        GameManager.instance.onHealthChange.AddListener(ChangeHealthUI);
        ChangeHealthUI(); // set initial value
    }

    void ChangeHealthUI()
    {
        image.sprite = spritesList[GameManager.instance.getLife()];
    }

    void OnDestroy()
    {
        GameManager.instance.onHealthChange.RemoveListener(ChangeHealthUI);
    }
}
