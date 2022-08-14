using System;
using UnityEngine;
using AIStateManager;
using Player;

namespace Data.AI
{

   
    
    public abstract class State: MonoBehaviour
    {
        protected EnemyRandomPathFinding EnemyAi;
        protected PartyFollow AllyAi;
        
        
        private void Start()
        {
            gameObject.tag = transform.parent.tag;
            if (gameObject.CompareTag("Enemy"))
            {
                EnemyAi = GetComponentInParent<EnemyRandomPathFinding>();
            }
            
            else
            {
                AllyAi = GetComponentInParent<PartyFollow>();
            }
        }

        public abstract State RunCurrentState();
    }
}