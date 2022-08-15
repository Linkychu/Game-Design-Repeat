using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    public AudioMixer mixer;

    private bool muted;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void Volume(float amount)
    {
        
        Debug.Log("amount");
        AudioSource[] soundSources = FindObjectsOfType<AudioSource>();
        foreach (var sound in soundSources)
        {
            sound.volume = amount;
        }

    }
    
}
