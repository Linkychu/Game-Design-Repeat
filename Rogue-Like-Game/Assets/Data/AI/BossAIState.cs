using Enemy;
using Player;
using UnityEngine;

namespace Data.AI
{
    public class BossAIState : StateMachineBehaviour
    {
        private BossAI ai;
        public BossAI.BossStates EnterState;
        public BossAI.BossStates ExitState;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            ai = animator.GetComponent<BossAI>();
            ai.SwitchState(EnterState);
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            /*ai = animator.GetComponent<BossAI>();
            ai.SwitchState(ExitState);*/
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
          
               
            
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