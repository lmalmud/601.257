using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: Manages the ties between the HUD and the actual values tracked in game
*/

public class OverlayGuiController : MonoBehaviour
{
    
    [SerializeField] private UIDocument UIDoc;

    private Label m_HealthLabel;
    private Label m_MoneyLabel;
    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        if (gm == null)
        {
            Debug.Log("OverlayGuiController::Start(): GameManager is null");
        }
        else
        {
            gm.onHealthChange.AddListener(updateLife);
            gm.onMoneyChange.AddListener(updateBalance);
        }
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("Lives");
        m_MoneyLabel = UIDoc.rootVisualElement.Q<Label>("Balance");
        updateLife();
        updateBalance();
        
    }



    void updateLife()
    {
        m_HealthLabel.text = "Lives: " + gm.getLife();
    }

    void updateBalance()
    {
        m_MoneyLabel.text = "Balance: " + gm.getMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        gm.onHealthChange.RemoveListener(updateLife);
        gm.onMoneyChange.RemoveListener(updateBalance);
    }
}
