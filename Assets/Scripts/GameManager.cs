using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int life = 5;
    public UnityEvent onDeath;

    void Awake()
    {
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

    void OnDestroy()
    {
        var endPointDetector = GetComponent<EndPointDetection>();
        endPointDetector.onReachEnd.RemoveListener(loseLife);
    }
}
