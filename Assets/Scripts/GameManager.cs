using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int life = 5; //my wife and four beautiful children
    [SerializeField] private int money = 1000; //<3 government stipend
    //surely nothing bad will happen to my family
    
    public List<WaveSpawner> waves;
    public static GameManager instance;
    public UnityEvent onChanged;
    public UnityEvent onDeath;

    void Awake()
    {
        //no duplicate game managers!
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated WavesManager, ignoring this one", gameObject);
        }

        //gets a reference to teh endpoint detector and listens out for the onReachEnd event
        var endPointDetector = GameObject.Find("EndPoint").GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.AddListener(loseLife);
    }

    void Update()
    {
        if (life <= 0)
        {
            onDeath.Invoke(); //sends out the onDeath event message
            Destroy(gameObject);
        }
    }

    public int getLife()
    {
        return life;
    }
    public void loseLife()
    {
        life--;
    }

    public int getMoney()
    {
        return money;
    }
    //pass positive m to gain money, pass negative m to spend money
    public void changeMoney(int m)
    {
        money += m;
    }

    void OnDestroy()
    {
        var endPointDetector = GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.RemoveListener(loseLife);
    }
}
