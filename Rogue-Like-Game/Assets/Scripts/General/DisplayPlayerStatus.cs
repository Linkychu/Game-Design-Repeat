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

    private List<Button> buttons = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    private void OnEnable()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].color = PlayerManager.instance.playerSpriteColors[i];
            buttons.Add(icons[i].GetComponentInChildren<Button>());
            Transform player = PlayerManager.instance.players[i];
            CharacterClass playerStats = player.GetComponent<CharacterClass>();
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                $"Name : {player.name} \n Level: {playerStats.values.myStats.level} \n Health: {playerStats.values.myStats.currentHP} / {playerStats.values.myStats.MaxHP} \n Mana: {playerStats.values.myStats.currentMana} / {playerStats.values.myStats.maxMana}";
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
