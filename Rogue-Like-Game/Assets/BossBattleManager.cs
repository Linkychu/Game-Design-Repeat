using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBattleManager : MonoBehaviour
{
    [SerializeField]private BossTriggerScript TriggerScript;
    [SerializeField] private Slider BossSlider;

    private bool filledBar;

    public GameObject BossSoundObject;
    [SerializeField] private float Seconds = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        BossSoundObject.SetActive(false);
        TriggerScript.OnPlayerEnterTrigger += OnPlayerEnterTrigger;
        BossSlider.transform.parent.gameObject.SetActive(false);
        FindObjectOfType<BossClass>().OnBossDeath += OnBossDeath;
    }

    private void OnBossDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        StartCoroutine(FillHealthBar(Seconds));
        TriggerScript.OnPlayerEnterTrigger -= OnPlayerEnterTrigger;
    }

    void StartBattle()
    {
        Debug.Log("Start");
       
    }

    IEnumerator FillHealthBar(float seconds)
    {
        BossSoundObject.SetActive(true);
        BossSlider.transform.parent.gameObject.SetActive(true);
        float time = 0;

        
        while (time < seconds)
        {
            time += Time.unscaledDeltaTime;
            float lerpValue = time / seconds;
            BossSlider.value = Mathf.Lerp(0, 1, lerpValue);
            yield return null;
        }
        
        
       StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
