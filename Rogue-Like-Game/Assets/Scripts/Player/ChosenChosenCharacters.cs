using General;
using UnityEngine;



namespace Player
{
    [CreateAssetMenu(fileName = "ChosenCharacter", menuName = "Data/Character/Chooser", order = 0)]
    public class ChosenChosenCharacters : ScriptableObject
    {
        public CharacterData[] characters;

        public CharacterData chosenCharacter1;
        public CharacterData chosenCharacter2;
        public CharacterData chosenCharacter3;

        private GenerateAllies[] models;

        public void FindCharacters()
        {
            models = FindObjectsOfType<GenerateAllies>();
        }
        public void ReRollCharacters()
        {
            ReRollCharacter1();
            ReRollCharacter2();
            ReRollCharacter3();
        }

        public void ReRollCharacter1()
        {
            chosenCharacter1 = Instantiate(characters[RNG.RngCallRange(0, characters.Length)]);
            chosenCharacter1.Initialise();

        }
        
        public void ReRollCharacter2()
        {
            chosenCharacter2 = Instantiate(characters[RNG.RngCallRange(0, characters.Length)]);
            chosenCharacter2.Initialise();
        }
        
        public void ReRollCharacter3()
        {
            chosenCharacter3 = Instantiate(characters[RNG.RngCallRange(0, characters.Length)]);
            chosenCharacter3.Initialise();
        }
        
    }
    
    
}