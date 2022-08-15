using System;
using UnityEngine;

namespace General
{
    
    
    
    
    public class DamageManager : MonoBehaviour
    {
        public static DamageManager instance { get; set; }

        public int CritRate = 20;

        public float CritBonus = 1.5f;

        private float Critical;
        private void Awake()
        {
            instance = this;
        }

        public int DamageCalculator(int skillDamage, int BaseAttack, int EnemyBaseHp, int EnemyDefence, int BaseSpecial, int EnemySpecial, float AttackModifier, float DefenseModifier, float SpecialModifier, MoveType type, Affinities affinity, Affinities enemyAffinity , int BaseLevel, int enemyLevel, float enemyDefenseModifier, float EnemySpecialModifier)
        {
            float damageRate = UnityEngine.Random.Range(0.9f, 1);
            
            //player does the damage and checks whether or not what we hit is resisted by it
            float ratio = enemyAffinity.DamageMultiplier(affinity);
            // ReSharper disable once PossibleLossOfFraction

            int  attackDefRatio = type == MoveType.Physical ? Mathf.FloorToInt( (BaseAttack * AttackModifier) / EnemyDefence * enemyDefenseModifier) : Mathf.FloorToInt((BaseSpecial * SpecialModifier) / EnemySpecial * EnemySpecialModifier);

            int CritChance = UnityEngine.Random.Range(0, 100);
            if (CritChance < CritRate)
            {
                Critical = CritBonus;
            }

            else
            {
                Critical = 1;
            }
            
            int result =
                Mathf.FloorToInt(((((((2 * BaseLevel) / 5) + 2) * skillDamage * attackDefRatio) / 50) + 2) * ratio * damageRate * Critical); 
           
           
           
           /*skillDamage* ratio * differenceInLevel * AttackModifier * SpecialModifier) /
               (attackDefRatio * DefenseModifier * enemyDefenseModifier * EnemySpecialModifier) * damageRate * 100));
               */


           
            return result;



            //more damage the higher the difference in user level compared to the enemy
            //less damage the bigger the enemy defence
            //more damage the bigger the player attack
            //less damage the higher the base HP is
        }
    }
}