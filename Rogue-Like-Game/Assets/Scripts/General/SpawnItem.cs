using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnItem : MonoBehaviour
{
    public List<ItemClass> PossibleItems = new List<ItemClass>();
    private  List<ItemClass> NormalItems = new List<ItemClass>();
    private  List<ItemClass> KeyItems = new List<ItemClass>();

    
    private GameObject item;


    private int keyItemIndex = -1;
    //private Transform chestParent;
    private GameObject[] chests;
    private void Awake()
    {
       
    }


    private void Start()
    {
       
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Dungeon"))
            return;
        
        foreach (var nItem in PossibleItems)
        {
            if (nItem.type is ItemType.Regular or ItemType.Consumable or ItemType.Gold)
            {
                NormalItems.Add(nItem);
            }

            else
            {
                KeyItems.Add(nItem);
            }
        }

        StartCoroutine(Generate());
        


    }

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(4);
        chests  = GameObject.FindGameObjectsWithTag("Item");
        GenerateKeyItems();
    }
    void GenerateKeyItems()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Dungeon"))
            return;
        int failure = 0;
        int i = 0;
        List<GameObject> KeyChests = new List<GameObject>();
        while ((KeyChests.Count < KeyItems.Count) && failure < 5000)
        {
            var index = RNG.RngCallRange(0, chests.Length);
            if (keyItemIndex == index)
            {
                index = RNG.RngCallRange(0, chests.Length);
                failure++;
                
            }
            else
            {
                var Chest = chests[index].GetComponent<OpenItem>();
                Chest.item = KeyItems[i].item;
                Chest.type = ChestType.KeyChest;
                keyItemIndex = index;
                KeyChests.Add(chests[index]);
                Chest.itemClass = KeyItems[i];
                Chest.itemGenerated = true;
                i++;
            }
        }
        GenerateItems();

    }

    void GenerateItems()
    {
        List<OpenItem> items = new List<OpenItem>();
        foreach (var Chest in chests)
        {
            var openItem = Chest.GetComponent<OpenItem>();
            if (openItem.type == ChestType.normalChest)
            {
                items.Add(openItem);
            }
        }

        foreach (var item in items)
        {
            while (item.itemGenerated == false)
            {
                int randomIndex = RNG.RngCallRange(0, NormalItems.Count);
                int randomRange = RNG.RngCallRange(0, 100);
                if (randomRange < NormalItems[randomIndex].rateOfAppearing)
                {
                    item.item = NormalItems[randomIndex].item;
                    item.itemClass = NormalItems[randomIndex];
                    item.itemGenerated = true;
                }

                else
                {
                    randomRange = RNG.RngCallRange(0, 100);
                    randomIndex = RNG.RngCallRange(0, NormalItems.Count);
                }
            }
        }


        /*if (KeyItems.Count > 0)
        {
            CheckForMissingItems();
        }*/
    }

    


    

 
    
}
