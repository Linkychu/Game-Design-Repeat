using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{

    public GameObject loadingScreen;

    public Slider loadingScreenSlider;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        Debug.Log(sceneName);
        if (sceneName == "BattleScene")
        {
            PlayerManager.instance.ResetPositions();
            FindObjectOfType<BossClass>().OnBossDeath += OnBossDeath;
            
        }
        
        
    }
    
    private void DeathEvent()
    {
        StartCoroutine(LoadSceneAsync((3)));
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LoadBossScene();
            }
        }
    }
    
    public void LoadBossScene()
    {
       
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));

    }

    IEnumerator LoadSceneAsync(int index)
    {
         
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        
        loadingScreen.SetActive(true);  

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingScreenSlider.value = progress;
            yield return null;
        }
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnBossDeath()
    {
        StartCoroutine(LoadSceneAsync(4));
    }

    public void Retry()
    {
        StartCoroutine(LoadSceneAsync(0));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
    
    

