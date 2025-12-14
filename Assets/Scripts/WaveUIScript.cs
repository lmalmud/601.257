using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    Author: Teddy Starynski
    Date Created: 11/13/25
    Last Edited: 12/14/25
    Controls the UI which shows the player how much money they have.
    Updated by Lucy to chage from updating every frame to only when there is a modification.

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
        GameManager.instance.onDayStart.AddListener(UpdateWaveUI);
        UpdateWaveUI(); // set initial value
    }

    void UpdateWaveUI()
    {
        text.text = "Waves Left\n " + GameManager.instance.getWavesLeft();
    }

    void OnDestroy()
    {
        GameManager.instance.onDayStart.RemoveListener(UpdateWaveUI);
    }
}
