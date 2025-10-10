using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int life = 5; //my wife and four beautiful children
    [SerializeField] private int money = 1000; //<3 government stipend
    //surely nothing bad will happen to my family
    
    public List<WaveSpawner> waves;
    public List<Enemy> enemies;
    public static GameManager instance;
    public UnityEvent onChanged;

    public Light sun;

    private bool day;
    //public UnityEvent onDeath;

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
            //onDeath.Invoke(); //sends out the onDeath event message
            //Destroy(gameObject);
            SceneManager.LoadScene("LoseScene");
            
        }
    }

    public void addWave(WaveSpawner wave)
    {
        waves.Add(wave);
    }
    public void removeWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        checkWinCondition();
    }
    public void addEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void removeEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        checkWinCondition();
    }

    private void checkWinCondition()
    {

        if (life <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        else if (enemies.Count <= 0 && waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public int getLife()
    {
        return life;
    }
    public void loseLife()
    {
        life--;
        // if (life <= 0)
        // {
        //     //onDeath.Invoke(); //sends out the onDeath event message
        //     //Destroy(gameObject);
        //     SceneManager.LoadScene("LoseScene");
        // }
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
        //var endPointDetector = GetComponent<EndPointDetection>();
        //endPointDetector.onReachEnd.RemoveListener(loseLife);
    }
}
