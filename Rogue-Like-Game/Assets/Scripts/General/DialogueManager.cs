using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    public GameObject dialoguePanel;

    //[System.NonSerialized]
    public bool isDialoguePlaying;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    // Start is called before the first frame update

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

    void Start()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDialoguePlaying)
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ContinueStory();
        }
        
    }

    public void DisplayDialogue(TextAsset dialogue)
    {
        
        currentStory = new Story(dialogue.text);
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
       
    }
    
    public void DisplayNewItem(TextAsset dialogue, string Name, int amount)
    {
        
        currentStory = new Story(dialogue.text);
        currentStory.variablesState["item"] = Name;
        currentStory.variablesState["amount"] = amount;
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
       
    }
    


    void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = String.Empty;
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }

        else
        {
            ExitDialogueMode();
        }
    }
}
