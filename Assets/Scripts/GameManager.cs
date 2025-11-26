using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/*
    Manages global game quatities: 
        Life: life total, subtracting lives, handling death
        Money: money total, gaining money, losing money
        Wave/Enemy Array: keeps arrays of the total waves active and the total enemies active
        Day/Night cycle: manage lighting and time of day.
        Scenes: Changes to the loss or win screen when needed
            - also checks for win and loss conditions when appropriate
        

*/

public class GameManager : MonoBehaviour
{
    [Header("General Game Parameters")]
    [SerializeField] private int life = 5; //my wife and four beautiful children
    [SerializeField] private int money = 1000; //<3 government stipend
                                               //surely nothing bad will happen to my family

    public List<WaveSpawner> waves;
    public List<Enemy> enemies;
    public static GameManager instance;
    public UnityEvent onChanged;
    public Transform homeBase;

    [SerializeField] private Light sun;
    public Image clockBackground;
    private bool isDay;

    //length of a day in seconds
    [SerializeField] private float dayLength = 60;
    //how many times per second the sun's position is updated
    [SerializeField] private float sunRotateTicksPerSecond = 4;
    //how much the sun is rotated each 'sun tick' in degrees
    private float sunRotateFraction;
    //public UnityEvent onDeath;
    public UnityEvent onHealthChange;
    public UnityEvent onMoneyChange;

    public UnityEvent onDayStart;
    public UnityEvent onNightStart;
    
    public AudioController audioController;

    [SerializeField] private activatePanel losePanel;
    private bool losePanelActive;
    [SerializeField] private activatePanel winPanel;

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
        
        sunRotateFraction = 180 / (dayLength * sunRotateTicksPerSecond) ;



        //gets a reference to teh endpoint detector and listens out for the onReachEnd event
        var endPointDetector = GameObject.Find("EndPoint").GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.AddListener(loseLife);
        InvokeRepeating("rotateSun", 0, 1/sunRotateTicksPerSecond);
        InvokeRepeating("toggleDayNight", 0, dayLength);
        
        onDayStart.AddListener(giveStipend); 

        losePanelActive = false;

    }

    void Start()
    {
        losePanel = GameObject.Find("LoseScreen").GetComponent<activatePanel>();
        winPanel = GameObject.Find("WinScreen").GetComponent<activatePanel>();

        losePanel.deactivateThisPanel();
        winPanel.deactivateThisPanel();
    }

    void Update()
    {
        if (life <= 0)
        {
            //onDeath.Invoke(); //sends out the onDeath event message
            //Destroy(gameObject);

            if (!losePanelActive)
            {
                losePanel.activateThisPanel();
                losePanelActive = true;
            }
            
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
    
    public void giveStipend()
    {
        changeMoney(500);
    }

    private void checkWinCondition()
    {

        if (life <= 0 && losePanel != null)
        {
            losePanel.activateThisPanel();
        }
        else if (enemies.Count <= 0 && waves.Count <= 0 && winPanel != null)
        {
            winPanel.activateThisPanel();
        }
    }

    private void rotateSun()
    {
        sun.transform.Rotate(new Vector3(sunRotateFraction, 0, 0));
        clockBackground.transform.Rotate(new Vector3(0,0,-sunRotateFraction));
    }

    private void toggleDayNight()
    {
        isDay = !isDay;
        if (isDay)
        {
            // changeMoney(500);
            // audioController.startDay();
            onDayStart.Invoke();
        }
        else
        {
            // audioController.startNight();
            onNightStart.Invoke();
        }
        // Debug.Log(isDay);
    }

    public int getLife()
    {
        return life;
    }
    public void loseLife()
    {
        life--;
        onHealthChange.Invoke();
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
        onMoneyChange.Invoke();
    }

    public void OnGiveMoney(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        giveStipend();
    }

    public bool spendMoney(int cost)
    {
        if (cost > money) return false;
        money -= cost;
        onMoneyChange.Invoke();
        return true;
    }

    public int getWavesLeft()
    {
        return waves.Count;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuScreen()
    {
        SceneManager.LoadScene("Title");
    }
}
