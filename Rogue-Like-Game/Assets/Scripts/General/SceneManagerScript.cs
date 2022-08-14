using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string currentLevelName = String.Empty;
    public static SceneManagerScript instance { get; set; }

    public delegate void LevelDelegate();

    public LevelDelegate OnLevelLoaded;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadBossScene(string LevelName)
    {
        if (OnLevelLoaded != null)
        {
            OnLevelLoaded();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerManager.instance.ResetPositions();
    }
}
    
    

