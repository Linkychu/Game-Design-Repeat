using UnityEngine;
using static EnemyRandomPathFinding;
using AIStateManager;


namespace Data.AI
{
    
   
    public class ChaseState : State
    {
        public AttackState attackState;
        public IdleState IdleState => transform.parent.GetComponentInChildren<IdleState>();
        public override State RunCurrentState()
        {
            if (isInAttackRange())
            {
                switch (gameObject.tag)
                {
                    case "Enemy":
                        EnemyAi.SwitchState(enemyState.attack);
                        break;
                    default:
                        AllyAi.SwitchState(AllyState.Attacking);
                        break;
                }

                return attackState;
            }

            else
            {
                switch (gameObject.tag)
                {
                    case "Enemy":
                        if (EnemyAi.target != null)
                        {
                            EnemyAi.SwitchState(enemyState.chase);
                        }

                        else
                        {
                            IdleState.canSeeTarget = false;
                            return IdleState;
                            
                        }

                        break;
                    default:
                        if (AllyAi.enemyTarget != null)
                        {
                            AllyAi.SwitchState(AllyState.Chasing);
                        }

                        else
                        {
                            IdleState.canSeeTarget = false;
                            return IdleState;
                        }
                        break;
                }
                return this;
            }
            
        }

        public bool isInAttackRange()
        {
            switch (gameObject.tag)
            {
                case "Enemy" when EnemyAi != null:
                    return EnemyAi.IsPlayerInRange();
                case "Ally" when AllyAi != null:
                    return AllyAi.isTargetInRange();
                default:
                    return false;
            }
        }
    }
}