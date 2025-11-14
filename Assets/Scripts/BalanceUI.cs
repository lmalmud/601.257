using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // void changeBalance()
    // {
    //     text.text = "$ " + GameManager.instance.getMoney();
    // }

    void Update()
    {
        text.text = "$ " + GameManager.instance.getMoney();
    }

}
