using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.IO;


public class RNG : MonoBehaviour
{
    private static RNG RandomController { get; set; }
    public int InitSeed;
    [SerializeField]private bool useRandomSeed;



    public static Random random;
    private void Awake()
    {
        if (RandomController == null)
            
        {
            RandomController = this;
            DontDestroyOnLoad(gameObject);
                
        }
        
        else
        { 
            Destroy(gameObject);
        }
      
        GenerateRandomSeed();

    }

    public void GenerateRandomSeed()
    {
        if (useRandomSeed)
        {
            InitSeed = (int) System.DateTime.Now.Ticks;
        }
        
        
        random = new Random(InitSeed); 
        UnityEngine.Random.InitState(InitSeed);
       // PlayerPrefs.SetInt("Seed", InitSeed);
    }

    private void Update()
    {
        
    }

    public static Color RandomColour()
    {
        return new Color((float)GetRandomValue(), (float)GetRandomValue(), (float)GetRandomValue());
    }
    public static int RngCallRange(int lowerBound, int upperBound)
    {
        
        int randomValue = random.Next(lowerBound, upperBound);
        

      //  random.Next();
      return randomValue;
    }

    public static int GetRandomValue()
    {
        return random.Next();
    }
    
    
    
    



}
