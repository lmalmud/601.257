using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    Author: Teddy Starynski
    Date Created: 11/13/25
    Last Edited: 11/13/25
    Controls the UI which shows the player how much money they have.

*/

public class WaveUIScript : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        text.text = "Waves Left\n " + GameManager.instance.getWavesLeft();
    }


    void Update()
    {
        text.text = "Waves Left\n " + GameManager.instance.getWavesLeft();
    }
}
