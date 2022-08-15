using Enemy;
using Player;
using UnityEngine;

namespace Data.AI
{
    public class BossChaseState : StateMachineBehaviour
    {
        private BossAI ai;
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Attack1 = Animator.StringToHash("Attack1");

        private float attackTime = 0;
        private float attackTimeLimit = 3f;
         private int index;
        private Transform target;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (ai == null)
            {
                ai = animator.GetComponent<BossAI>();
                if (PlayerManager.instance == null)
                {
                    index = 0;
                    target = GameObject.FindObjectOfType<PlayerClass>().transform;

                }

                else
                {
                    index = UnityEngine.Random.Range(0, PlayerManager.instance.AliveAllies.Count);
                    target = PlayerManager.instance.AliveAllies[index];
                }
             
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.ResetTrigger(Attack);
            animator.ResetTrigger(Attack1);
            
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (attackTime < attackTimeLimit)
            {
                if (!ai.isGrounded)
                    return;
                if (Vector3.Distance(ai.fireBreathPos.position, target.position) <= ai.AttackRange)
                {
                    int random = Random.Range(0, 2);

                    if (random == 0)
                    {
                        attackTime += Time.deltaTime;
                        ai.SetTarget(target);
                        animator.SetTrigger(Attack);
                        
                    }

                    else
                    {
                        attackTime += Time.deltaTime;
                        ai.SetTarget(target);
                        animator.SetTrigger(Attack1);
                    }
                }

                else
                {
                    ai.agent.destination = target.position;
                }

                Vector3 targetPos = target.position;
                Vector3 npcPos = ai.transform.position;
                Vector3 delta = (targetPos - npcPos).normalized;
                Quaternion rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
                ai.transform.rotation = Quaternion.RotateTowards(ai.transform.rotation, rotation, Time.deltaTime * 100f);
                Debug.Log(attackTime);
            }

            else
            {
                attackTime = 0;
                animator.Play("Take Off");
            }


        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}