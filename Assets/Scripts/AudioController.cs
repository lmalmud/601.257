using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip nightStart;
    public AudioClip dayStart;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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
}
