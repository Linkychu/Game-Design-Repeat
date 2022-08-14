using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyClass : CharacterClass
    {
        
        private void Awake()
        {
        
            values = (CharacterValues) Instantiate(values);
            charBase = (CharacterBase) Instantiate(charBase);
        }
        public int expAmount;
        public override void Death()
        {
            PlayerClass player = FindObjectOfType<PlayerClass>();
            player.level.AddExp(Mathf.FloorToInt((expAmount * charBase.BaseHP * values.myStats.level) / (7f * PlayerManager.instance.AliveAllies.Count)));
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}