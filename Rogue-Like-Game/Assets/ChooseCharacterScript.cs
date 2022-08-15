using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterScript : MonoBehaviour
{
    public ChosenChosenCharacters charactersRef;

    public GameObject model1;

    public GameObject model2;

    public GameObject model3;

    private int Index = 0;

    public GameObject loadingScreen;

    public Slider loadingScreenSlider;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
       charactersRef.ReRollCharacters();
       
       
    }

    public void DisplayStats()
    {
        switch (Index)
        {
            case 0:
                model1.GetComponentInChildren<TextMeshProUGUI>().text = $" Name : {charactersRef.chosenCharacter1.model.name} Level :{charactersRef.chosenCharacter1.values.myStats.level} Type: {charactersRef.chosenCharacter1.CharacterBase.type.affinity.ToString()}";
                Index = 1;
                break;
            case 1:
                model2.GetComponentInChildren<TextMeshProUGUI>().text = $" Name : {charactersRef.chosenCharacter2.model.name} Level :{charactersRef.chosenCharacter2.values.myStats.level} Type: {charactersRef.chosenCharacter2.CharacterBase.type.affinity.ToString()}";
                Index = 2;
                break;
            case 2:
                model3.GetComponentInChildren<TextMeshProUGUI>().text = $" Name : {charactersRef.chosenCharacter3.model.name} Level :{charactersRef.chosenCharacter3.values.myStats.level} Type: {charactersRef.chosenCharacter3.CharacterBase.type.affinity.ToString()}";
                Index = 0;
                break;


        }
    }

    public void Confirm()
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
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReRoll(int index)
    {
        switch (index)
        {
            default:
                Index = 0;
                charactersRef.ReRollCharacter1();
                break;
            case 2:
                Index = 1;
                charactersRef.ReRollCharacter2();
                break;
            case 3:
                Index = 2;
                charactersRef.ReRollCharacter3();
                break;
            case 4:
                Index = 0;
                charactersRef.ReRollCharacters();
                break;
        }
    }
}
