using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum ChestType
{
    normalChest,
    KeyChest
}
public class OpenItem : MonoBehaviour
{
    public Mesh closed;
    public Mesh  open;
    
    private bool isOpened;
    
    public LayerMask playerLayer;

    private MeshFilter _filter;

    [System.NonSerialized]
    public GameObject item;

    [System.NonSerialized]
    public bool itemGenerated;

    [System.NonSerialized]
    public ChestType type;

    [System.NonSerialized]
    public ItemClass itemClass;

    [SerializeField] private TextAsset dialogueText;

 
    

    /*public int itemRate = 80;
    private int itemChance => RNG.RngCallRange(0, 100);*/

    private void Start()
    {
        
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = closed;
        isOpened = false;
    }

    private void Update()
    {
        bool isPlayerDected = Physics.CheckSphere(transform.position, 2, playerLayer, QueryTriggerInteraction.Collide);

        bool Epressed = Input.GetKeyDown(KeyCode.E);
        if (isPlayerDected && !isOpened && Epressed)
        {
            StartCoroutine(OpenChest());
            // GenerateItem();
        }
    }
    
    
    IEnumerator OpenChest()
    {
       
        if (itemGenerated)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 0;

            _filter.mesh = open;

            isOpened = true;

            GameObject ins = Instantiate(item, new Vector3(transform.position.x, item.transform.position.y, transform.position.z), item.transform.rotation);

            var position = ins.transform.position;
            position = new Vector3(position.x, position.y + 2, position.z);
            ins.transform.position = position;

            int amount = RNG.RngCallRange(itemClass.minAmount, itemClass.maxAmount);
            amount = Mathf.Clamp(amount, 1, itemClass.maxAmount);
            DialogueManager.instance.DisplayNewItem(dialogueText, itemClass.name, amount);
            while (DialogueManager.instance.isDialoguePlaying)
            {
                yield return null;
                //yield return new WaitUntil(() => DialogueManager.instance.isDialoguePlaying = false);
            }
            Destroy(ins);
            Time.timeScale = 1;
            InventoryManager.instance.AddItem(itemClass, amount);
            if (itemClass.type == ItemType.Map)
            {
                GameManager.instance.CheckForMap(true);
            }

            else if(itemClass.type == ItemType.BossKey)
            {
                GameManager.instance.CheckForKey(true);
            }
            
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}