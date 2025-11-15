using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
