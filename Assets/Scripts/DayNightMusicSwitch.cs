using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lucy Malmud
    Date Created: 11/27/2025
    Date Last Updated: 11/27/2025
    Summary: Changes the music between when it is day and when it is night.
*/

public class DayNightMusicSwitch : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip dayMusic;
    [SerializeField] private AudioClip nightMusic;
    [SerializeField] private float fadeDuration = 2f;
    
    [Header("Debug Info")]
    [SerializeField] private string currentTrack = "None";
    [SerializeField] private bool isPlaying = false;
    
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;
    
    void Start()
    {
        // get AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // set up background music
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        
        // get day/night events from GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.onDayStart.AddListener(PlayDayMusic);
            GameManager.instance.onNightStart.AddListener(PlayNightMusic);
        }
        
        // start with day music
        PlayDayMusic();
    }
    
    void Update()
    {
        // update debug info in inspector
        isPlaying = audioSource.isPlaying;
        if (audioSource.clip != null)
        {
            currentTrack = audioSource.clip.name;
        }
        else
        {
            currentTrack = "None";
        }
    }
    
    void OnDestroy()
    {
        // delete when done
        if (GameManager.instance != null)
        {
            GameManager.instance.onDayStart.RemoveListener(PlayDayMusic);
            GameManager.instance.onNightStart.RemoveListener(PlayNightMusic);
        }
    }
    
    public void PlayDayMusic()
    {
        if (dayMusic != null)
        {
            Debug.Log("Switching to Day Music: " + dayMusic.name);
            SwitchMusic(dayMusic);
        }
        else
        {
            Debug.LogWarning("Day music clip is not assigned!");
        }
    }
    
    public void PlayNightMusic()
    {
        if (nightMusic != null)
        {
            Debug.Log("Switching to Night Music: " + nightMusic.name);
            SwitchMusic(nightMusic);
        }
        else
        {
            Debug.LogWarning("Night music clip is not assigned!");
        }
    }
    
    private void SwitchMusic(AudioClip newClip)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        fadeCoroutine = StartCoroutine(FadeToNewMusic(newClip));
    }
    
    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        float startVolume = audioSource.volume;
        
        // fade out current music
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        
        // switch to new clip
        audioSource.clip = newClip;
        audioSource.Play();
        Debug.Log("Now playing: " + newClip.name);
        
        // fade in new music
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        
        audioSource.volume = startVolume;
    }
}