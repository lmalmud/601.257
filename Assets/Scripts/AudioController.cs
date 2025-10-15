using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/1/25
    Summary: Managing script for all the current (very limited) audio in the game. Listens to a variety of events and 
    kicks off playing sounds accordingly
*/

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioClip nightStart;
    [SerializeField] private AudioClip dayStart;
    
    
    
    private AudioSource audioSource;

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gm = GameManager.instance;
        gm.onDayStart.AddListener(startDay);
        gm.onNightStart.AddListener(startNight);
    }

    public void startNight()
    {
        audioSource.clip = nightStart;
        audioSource.Play();

    }

    public void startDay()
    {
        audioSource.clip = dayStart;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        gm.onDayStart.RemoveListener(startDay);
        gm.onNightStart.RemoveListener(startNight);
    }
}
