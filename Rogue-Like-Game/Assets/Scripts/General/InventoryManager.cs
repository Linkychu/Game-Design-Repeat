using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    private Dictionary<ItemClass, int> Inventory = new Dictionary<ItemClass, int>();
    
    private bool OpenedInventory = false;
    public GameObject inventoryCanvas;

    [SerializeField]private Transform buttonParent;

    private List<Button> InventorySlots = new List<Button>();

    public delegate void InventoryOpen();

    public InventoryOpen onInventoryOpen;

    [SerializeField] Transform inventoryScreens;
    
    

    private int nextIndex;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        foreach (Transform image in buttonParent)
        {
            Button button = image.GetComponentInChildren<Button>();
            InventorySlots.Add(button);
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = String.Empty;
        }

        /*else
        {
            Destroy(gameObject);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInventory();

        }
    }

    void UpdateSlots()
    {
        
        foreach (var button in InventorySlots)
        {
            button.interactable = false;
            button.GetComponent<InventoryIcons>().Open(null,  false);
            button.GetComponentInChildren<TextMeshProUGUI>().text = String.Empty;
        } 
        for (int i = 0; i < Inventory.Count; i++)
        {
            var slot = InventorySlots[i].GetComponent<InventoryIcons>();
            InventorySlots[i].interactable = true;
            InventorySlots[i].GetComponentInChildren<TextMeshProUGUI>().text = Inventory.ElementAt((Inventory.Count -1) - i).Value.ToString();
            slot.Open(Inventory.ElementAt((Inventory.Count -1)  - i).Key, true);

        }
    }
    
    
    void OpenInventory()
    {
        
       
        OpenedInventory = !OpenedInventory;
        onInventoryOpen();
        UpdateSlots();
        inventoryCanvas.SetActive(OpenedInventory);
        //Cursor.visible = OpenedInventory;
        if (Cursor.lockState == CursorLockMode.Locked && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Time.timeScale = (!OpenedInventory).GetHashCode(); 
        
        
        
    }

    public void AddItem(ItemClass item, int amount)
    {
        if (Inventory.ContainsKey(item) == false)
        {
            Inventory.Add(item, amount);
        }

        else
        {
            Inventory[item] += amount;
            Mathf.Clamp(Inventory[item], 0, 99);
        }
        UpdateSlots();
    }

    public void RemoveItem(ItemClass item, int amount)
    {
        if(!Inventory.ContainsKey(item))
            return;
        Inventory[item] -= amount;

        if (Inventory[item] <= 0)
        {
            Inventory.Remove(item);
        }
        
        UpdateSlots();
    }


   
    public void OnNextClicked()
    {

        int i = 0;

        if (nextIndex >= inventoryScreens.childCount - 1)
        {
            nextIndex = 0;
        }

        else
        {
            nextIndex++;
        }
      

        foreach (Transform child in inventoryScreens)
        {
            if (i == nextIndex)
            {
                child.gameObject.SetActive(true);
            }

            else
            {
                child.gameObject.SetActive(false);
            }

            i++;
        }
        
      

    }
}
