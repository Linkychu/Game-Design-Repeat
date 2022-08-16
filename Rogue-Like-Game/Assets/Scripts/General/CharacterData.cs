using Player;
using UnityEngine;
using UnityEngine.AI;

namespace General
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public GameObject model;
        public CharacterBase CharacterBase;
        public CharacterValues values;
        public int MinLevel;
        public int MaxLevel;

        public int Level;

        
        


        public void Initialise()
        {
            CharacterBase = (CharacterBase) Instantiate(CharacterBase);
           Level = values.myStats.level = RNG.RngCallRange(MinLevel, MaxLevel + 1);
            FindObjectOfType<ChooseCharacterScript>().DisplayStats();
        }
        public void Spawn(Vector3 InitialPos, Vector3 finalPos, Transform parent, int index)
        {
            //CharacterBase = (CharacterBase) Instantiate(CharacterBase);
            GameObject Ally = Instantiate(model, InitialPos,  Quaternion.identity, parent);
            Ally.GetComponent<NavMeshAgent>().Warp(finalPos);
            Ally.GetComponent<AllyClass>().values = values;
            Ally.GetComponent<AllyClass>().Level = Level;
            Ally.GetComponent<AllyClass>().SetUp();
            Ally.GetComponent<AllyClass>().OnLevelUp();
             Ally.GetComponent<PartyFollow>().index = index;
            
        }
    }
}