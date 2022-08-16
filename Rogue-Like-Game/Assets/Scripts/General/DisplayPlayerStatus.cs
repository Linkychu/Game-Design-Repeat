using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStatus : MonoBehaviour
{
    public List<Image> icons = new List<Image>();

    public List<Button> buttons = new List<Button>();
    // Start is called before the first frame update
    void Awake()
    {
        
    }


    public void OnEnable()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            
            Transform player = PlayerManager.instance.players[i];
           var playerStats = i == 0 ? player.GetComponent<CharacterClass>() : player.GetComponent<AllyClass>();
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                $"Name : {player.name} \n Level: {playerStats.values.myStats.level} \n Health: {playerStats.values.myStats.currentHP} / {playerStats.values.myStats.MaxHP} \n Mana: {playerStats.values.myStats.currentMana} / {playerStats.values.myStats.maxMana}";
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
