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

public class BalanceUI : MonoBehaviour
{
    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        //GameManager.instance.onMoneyChange.AddListener(changeBalance);
    }

    void Start()
    {
        text.text = "$ " + GameManager.instance.getMoney();
    }


    void Update()
    {
        text.text = "$ " + GameManager.instance.getMoney();
    }

}
