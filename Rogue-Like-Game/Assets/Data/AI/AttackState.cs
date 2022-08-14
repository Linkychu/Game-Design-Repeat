using System;
using System.Collections;
using AIStateManager;
using UnityEngine;
using static EnemyRandomPathFinding;

namespace Data.AI
{
    public class AttackState : State
    {
        private bool canAttack = true;
        public float CoolDownDuration = 1f;
        public AttackEffect effect;
        public AttackEffect[] effects;
        public CharacterClass charClass;
        public Vector3 offset;
        public ChaseState chaseState;
        public IdleState idleState;

        public float effectTime = 1f;
        private float time;

        private void Awake()
        {
            idleState = transform.parent.GetComponentInChildren<IdleState>();
        }

        public override State RunCurrentState()
        {
            
            switch (gameObject.tag)
            {
                case "Enemy":
                    if (!EnemyAi.IsPlayerInRange() || EnemyAi.target == null)
                    {
                        EnemyAi.SwitchState(enemyState.chase);
                        return chaseState;
                    }
                    
                    else if (EnemyAi.target != null)
                    {
                        StartCoroutine(Attack());
                        return this;
                    }
                    
                    break;
                default:
                    if (!AllyAi.isTargetInRange() || AllyAi.enemyTarget == null)
                    {
                        AllyAi.SwitchState(AllyState.Chasing);
                        return chaseState;
                    }

                    else 
                    {
                        StartCoroutine(Attack());
                        return this;
                    }

            }

            return chaseState;
        }

        IEnumerator Attack()
        {
            if (canAttack)
            {
                AttackEffect effectObj;
                switch (gameObject.tag)
                {
                    case "Enemy":
                        EnemyAi.SwitchState(enemyState.attack);
                        charClass = EnemyAi.gameObject.GetComponent<CharacterClass>();
                        if (!(charClass.values.myStats.currentMana >= effect.move.manaCost))
                        {
                            yield return null;
                        }

                        charClass.values.myStats.currentMana -= effect.move.manaCost;
                        CoolDownDuration = effectTime + effect.move.lifetime;
                        effectObj = Instantiate(effect, EnemyAi.target.position + offset, transform.rotation);
                        break;
                    default:
                        AllyAi.SwitchState(AllyState.Attacking);
                        charClass = AllyAi.gameObject.GetComponent<CharacterClass>();

                        int index = UnityEngine.Random.Range(0, effects.Length);
                        if (effects.Length >= 1)
                        {
                            if ((charClass.values.myStats.currentMana < effects[index].move.manaCost) || (charClass.values.myStats.currentMana <= 0))
                            {
                                yield return null;
                            }

                            charClass.UseMana(effects[index].move.manaCost);
                            effectObj = Instantiate(effects[index], AllyAi.enemyTarget.position, transform.rotation);
                            CoolDownDuration = effectTime + effects[index].move.lifetime;
                        }
                        else
                        {
                            effectObj = Instantiate(effect, AllyAi.enemyTarget.position, transform.rotation);
                            CoolDownDuration = effectTime + effect.move.lifetime;
                        }
                        break;
                }
               
               
                
               
                effectObj.SetUser(charClass);
                StartCoroutine(CoolDownTimer());
            }

            else
            {
                yield return new WaitUntil(() => canAttack);
            }
        }
        
        IEnumerator CoolDownTimer()
        {
            canAttack = false;
            if (gameObject.CompareTag("Ally"))
            {
                yield return new WaitForSeconds(CoolDownDuration);

            }

            else
            {
                yield return new WaitForSeconds(CoolDownDuration * 2);

            }
          
           
            canAttack = true;


        }
    }
}