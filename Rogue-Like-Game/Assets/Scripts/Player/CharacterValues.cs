using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [CreateAssetMenu(fileName = "StatValues", menuName = "Data/StatValue", order = 0)]
    public class CharacterValues : ScriptableObject
    {
        [System.Serializable]
        public struct Stats
        {
            public int level;
            public string userName;
            public int MaxHP;
            public int currentHP;
            public int currentMana;
            public bool isDead;
            public int maxMana;

            public int Attack;
            public int Defense;
            public int Special;
            public int Speed;
           
        }
        
        public Stats myStats;
    }
    
    
}