using Player;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "Boss", menuName = "Data/Boss", order = 0)]
    public class BossEnemy : ScriptableObject
    {
        public CharacterBase charBase;
        public GameObject model;
        public CharacterValues values;
        public AttackEffect[] moves;
    }
}