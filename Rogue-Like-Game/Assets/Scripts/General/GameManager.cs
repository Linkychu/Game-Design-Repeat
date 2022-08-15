using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float time = 10;
    private float countDownTimer;

    public bool hasKey;
    public bool hasMap;
    private bool canCountDown = false;

    public GameObject miniMap;
    private GameObject bossDoor;

   

    public enum BossEnum
    {
        Dragon,
        Troll,
        Syndra
    };
    
    
    public BossEnum GeneratedBoss;

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

    private void Start()
    {
        instance = this;
        time *= 60;
        countDownTimer = time;
        canCountDown = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<PlayerClass>().deathEvent += OnPlayerDeath;
        GeneratedBoss = (BossEnum)RNG.RngCallRange(0, 3);
    }

    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        CountDown();
        
    }



    
    public void CheckForMap(bool val)
    {
        if (val == true)
        {
            hasMap = true;
            miniMap.SetActive(true);
        }
    }
    
    public void CheckForKey(bool val)
    {
        if (val)
        {
            hasKey = true;
            bossDoor = GameObject.FindGameObjectWithTag("BossKeyDoor");
            bossDoor.SetActive(false);
        }
    }

    void OnPlayerDeath()
    {
        Time.timeScale = 0;
        Debug.Log("Died");
    }
    void CountDown()
    {
        if (canCountDown)
        {
            countDownTimer -= Time.deltaTime;
            countDownTimer = Mathf.Clamp(countDownTimer, 0, time);

            float minutes = Mathf.FloorToInt(countDownTimer / 60);
            float seconds = Mathf.FloorToInt(countDownTimer % 60);

            text.text = $"{minutes:00} : {seconds:00}";
        }
    }
}
