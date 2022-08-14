using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIcons : MonoBehaviour
{
    private bool selected;
   private ItemClass item;

    public GameObject textBox;

    public Image sprite;

    public TextMeshProUGUI descriptionText;
    private Button _button;
    public GameObject PromptBox;

    public GameObject targetPlayerBox;
    public List<Button> Buttons = new List<Button>();
    private Sprite _image;

    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        textBox.SetActive(false);
        PromptBox.SetActive(false);
        targetPlayerBox.SetActive(false);

        if (!spawned)
        {
            _image = sprite.sprite;
        }

    }

    public void Open(ItemClass itemClass, bool validItem)
    {
        if (validItem)
        {
            item = itemClass;
            sprite.sprite = item.image;
            spawned = true;
        }
        else
        {
            item = null;
            sprite.sprite = _image;
        }


    }


    private void OnDisable()
    {
        PromptBox.SetActive(false);
        textBox.SetActive(false);
        descriptionText.text = String.Empty;
        targetPlayerBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        if(!_button.interactable)
            return;
        textBox.SetActive(true);
        descriptionText.text = item.description;
    }

    public void OnHoverExit()
    {
        if(!_button.interactable)
            return;
        textBox.SetActive(false);
        descriptionText.text = String.Empty;
    }

    public void OnItemClicked()
    {
        if(item.type != ItemType.Consumable)
            return;
        else
        {
            PromptBox.SetActive(true);
        }
    }
    
    public void UseItem(bool value)
    {
        if (value)
        {
            
            PromptBox.SetActive(false);
            OpenItemPrompt();
        }

        else
        {
            PromptBox.SetActive(false);
        }
    }

    void OpenItemPrompt()
    {
        int index = 0;
        targetPlayerBox.SetActive(true);
        foreach (Button button in Buttons)
        {
            button.interactable = false;
            Transform player = PlayerManager.instance.players[index];
            CharacterClass playerStats = player.GetComponent<CharacterClass>();
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                $"Name : {player.name} \n Level: {playerStats.values.myStats.level} \n Health: {playerStats.values.myStats.currentHP} / {playerStats.values.myStats.MaxHP} \n Mana: {playerStats.values.myStats.currentMana} / {playerStats.values.myStats.maxMana}";

            if (PlayerManager.instance.AliveAllies.Contains(player))
            {
                button.interactable = true;
            }
            index++;
            
        }
    }

    public void UseItem(int index)
    {
        
        
        CharacterClass player = PlayerManager.instance.players[index].GetComponent<CharacterClass>();

        switch (item.item.tag)
        {
            case "Potion":
                if(player.values.myStats.currentHP == player.values.myStats.MaxHP)
                    break;
                player.Heal(item.HealAmount);
                break;  
            case "Mana":
                if(player.values.myStats.currentMana == player.values.myStats.maxMana)
                    break;
                player.HealMana(item.HealAmount);
                break;
            case "StatBoost":
                PlayerManager.instance.players[index].GetComponent<IBoostable>().BoostStats(item.HealAmount, item.Boost);
                break;
            default:
                BackOut();
                break;
            
            
        }

        InventoryManager.instance.RemoveItem(item, 1);
       BackOut();
        
    }

    public void BackOut()
    {
        
        targetPlayerBox.SetActive(false);
    }
    
    
    
}
