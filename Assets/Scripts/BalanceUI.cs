using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    Author: Teddy Starynski
    Date Created: 11/13/25
    Last Edited: 12/14/25
    Controls the UI which shows the player how much money they have.
    Edited by Lucy to minimize updates to screen
*/

public class BalanceUI : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        GameManager.instance.onMoneyChange.AddListener(UpdateBalance);
        UpdateBalance(); // set initial value
    }

    void UpdateBalance()
    {
        text.text = "$ " + GameManager.instance.getMoney();
    }

    void OnDestroy()
    {
        GameManager.instance.onMoneyChange.RemoveListener(UpdateBalance);
    }
}
