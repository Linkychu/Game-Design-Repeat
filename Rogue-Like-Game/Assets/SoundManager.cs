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
        if (!muted)
        {
            Debug.Log("amount");
            mixer.SetFloat("Volume", amount);
            
        }
    }

    public void MuteSounds(bool value)
    {
        if (value)
        {
            mixer.SetFloat("Volume", 0);
        }
        
        muted = value;

    }
}
