
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using AIStateManager;
using Data.AI;


public enum AllyState 
{
    Idle,
    Chasing,
    Attacking
};



namespace Player
{
    public class PartyFollow : MonoBehaviour
    {
      
        private Transform playerTarget;

        [System.NonSerialized]
        public int index;

        private NavMeshAgent Agent;

        private Transform target;

        
        public Transform enemyTarget;

        
        private Transform player;

        public float EnemyDetectionRadius = 2;

        public LayerMask enemyLayer;
        

        public float angle = 60f;

        [SerializeField] private int enemyDetectionAmount;

        private Animator _animator;
     
        private AllyState state;

        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private CharacterController playerController;

        public float speedRate = 2f;

        private float speed;
        

        private float agentSpeed;

        private StateManager stateManager;
        // Start is called before the first frame update
        void Start()
        {

            stateManager = GetComponent<StateManager>();
            playerTarget = PlayerManager.instance.slots[index];
            Agent = GetComponent<NavMeshAgent>();
            player = FindObjectOfType<PlayerClass>().transform;
            speed = FindObjectOfType<ThirdPersonController>().targetSpeed;
            playerController = player.GetComponent<CharacterController>();
            agentSpeed = Agent.speed;
            Agent.acceleration = 100;

            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            // target = playerTarget;
        }

        

        void Idle()
        {
            
            
            
            
            if (_animator != null)
            {
               if(Agent.velocity.magnitude == 0)
               {
                    _animator.SetBool(IsWalking, false);
               }
                
               

               else
               {
                    _animator.SetBool(IsWalking, true);
                    
               }
            }
            
            
            
            if (IsPlayerInRange()) return;
            Vector3 targetPos = playerTarget.position;
            Vector3 npcPos = transform.position;

            Vector3 delta = (targetPos - npcPos).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 100f);
            Agent.speed = agentSpeed + (1 * playerController.velocity.magnitude);
            Agent.destination = targetPos;

            if (Vector3.Distance(npcPos, targetPos) >= 50)
            {
                Agent.Warp(new Vector3(targetPos.x, npcPos.y, targetPos.z));
            }


        }

        private bool IsPlayerInRange()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position), out hit, 2))
                    if (hit.transform == player)
                        return true;
            return false;
        }

        /*Transform Target = isEnemyInSight() ? enemyTarget : player;
        
        */

        public void ChaseTarget()
        {
            if (!isTargetInRange())
            {
                _animator.SetBool(IsWalking, true);
                
                if (enemyTarget != null)
                {
                    Agent.SetDestination(enemyTarget.position);
                }

                else
                {
                    
                    stateManager.currentState = stateManager.GetComponentInChildren<IdleState>();
                    stateManager.GetComponentInChildren<IdleState>().canSeeTarget = false;
                }
                
                

            }
            


        }


        public bool isTargetInRange()
        {
            if (enemyTarget != null)
            {
                return Vector3.Distance(transform.position, enemyTarget.position) <= EnemyDetectionRadius;
                
            }

            else
            {
                enemyTarget = null;
                stateManager.currentState = stateManager.gameObject.GetComponentInChildren<IdleState>();
                SwitchState(AllyState.Idle);
                return false;
            }

           
        }
        // Update is called once per frame
        void LateUpdate()
        {
           
        }

        void UpdatePlayerStates()
        {
            switch (state)
            {
                case AllyState.Idle:
                    Idle();
                    break;
                case AllyState.Chasing:
                    if (enemyTarget == null)
                    {
                        SwitchState(AllyState.Idle);
                    }
                    else
                    {
                        ChaseTarget();
                    }
                   
                    break;
                case AllyState.Attacking:
                    if (enemyTarget == null)
                    {
                        SwitchState(AllyState.Idle);
                    }

                    else
                    {
                        Attack();
                    }

                  
                    break;
            }
        }


        private void Update()
        {
           
            UpdatePlayerStates();
            
        }

       public bool isEnemyInSight()
        {
            Collider[] sightCol = new Collider[1];
            int results = Physics.OverlapSphereNonAlloc(transform.position, EnemyDetectionRadius, sightCol, enemyLayer);
            if (results > 0)
            {
                
                enemyTarget = sightCol[0].transform;
                return true;
                
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, EnemyDetectionRadius);
            
        }
        
        public void SwitchState (AllyState allyState)
        {
            state = allyState;
        }

        void Attack()
        {
            transform.LookAt(enemyTarget);
            _animator.SetTrigger("Attack");
        }
    
    }
}
