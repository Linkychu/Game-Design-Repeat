using AIStateManager;
using UnityEngine;
using static EnemyRandomPathFinding;

namespace Data.AI
{
    public class IdleState : State
    {
        public ChaseState chaseState;
        public bool canSeeTarget;
        


        public override State RunCurrentState()
        {
            
            
            if (canSeeTarget)
            {
                switch (gameObject.tag)
                {
                    case "Enemy":
                        EnemyAi.SwitchState(enemyState.chase);
                        break;
                    case "Ally":
                        AllyAi.SwitchState(AllyState.Chasing);
                        break;
                }

                return chaseState;
            }

            else
            {
                LookForTarget();
                return this;
            }
           
        }


        void LookForTarget()
        {
            
            if (gameObject.CompareTag("Enemy") && EnemyAi != null)
            {
                EnemyAi.SwitchState(enemyState.patrol);
                
                canSeeTarget = EnemyAi.IsPlayerVisible();
                    
            }
            
            else if (gameObject.CompareTag("Ally") && AllyAi != null)
            {
                AllyAi.SwitchState(AllyState.Idle);
                canSeeTarget = AllyAi.isEnemyInSight();
            }
        }
    }
}