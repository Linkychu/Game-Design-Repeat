using System;
using General;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BossAI : MonoBehaviour
    {
        public BossEnemy myEnemy;
        private Animator _animator;
        private int animLayer = 0;
        private bool canStart;

        private AttackEffect currentAttackEffect;

        private bool isFire;

        internal Transform fireBreathPos;

        private BossClass myClass;
        private static readonly int IsDead = Animator.StringToHash("isDead");

        public enum BossStates
        {
            Idle,
            Chasing,
            Attacking
        }

        public float AttackRange = 3f;

        public BossStates state;

        internal NavMeshAgent agent;
        private static readonly int Attack = Animator.StringToHash("Attack");

        internal bool isGrounded;

        public LayerMask GroundLayer;

       
        public Transform groundCheckTransform;
        [SerializeField] private Vector3 offset;

        private Transform playerTarget;
        private void Start()
        {
            myEnemy = (BossEnemy) Instantiate(myEnemy);
            FindObjectOfType<BossTriggerScript>().OnPlayerEnterTrigger += OnPlayerEnterTrigger;
            _animator = GetComponent<Animator>();
            _animator.speed = 0;
            fireBreathPos = GameObject.Find("FireBreathPosition").transform;
            myClass = GetComponent<BossClass>();
            agent = GetComponent<NavMeshAgent>();
            myClass.OnBossDeath += OnBossDeath;
        }

        private void OnBossDeath()
        {
            _animator.SetBool(IsDead, true);
        }

        private void OnPlayerEnterTrigger(object sender, EventArgs e)
        {
            StartFight();
        }

        public void SwitchState(BossStates T_state)
        {
            state = T_state;
        }
        private void StartFight()
        {
            _animator.speed = 1;
            canStart = true;
        }

        private void Update()
        {

            isGrounded = Physics.CheckSphere(groundCheckTransform.position, 2, GroundLayer);
            if (isFire && currentAttackEffect != null)
            {
                currentAttackEffect.transform.position = fireBreathPos.position;
            }
            

            /*switch (state)
            {
                case BossStates.Idle:
                    Idle();
                    break;  
                case BossStates.Chasing:
                    Chase();
                    break;
                case BossStates.Attacking:
                    AttackTarget();
                    break;
            }*/
        }
        
        



        void Idle()
        {
            
        }

        public void Chase()
        {
           
        }

        public void SetTarget(Transform target)
        {
            playerTarget = target;
        }

        public void ResetTarget()
        {
            
            playerTarget = null;
            
        }
        public void AttackTarget(int index)
        {
            if (playerTarget != null)
            {
                currentAttackEffect =
                    Instantiate(myEnemy.moves[index], playerTarget.transform.position, Quaternion.identity);
                currentAttackEffect.SetUser(myClass);
            }
        }
        public void FlameAttackOn()
        {
            if (currentAttackEffect != null && isFire)
                return;
            currentAttackEffect = Instantiate(myEnemy.moves[0], new Vector3(fireBreathPos.position.x, fireBreathPos.position.y - offset.y, fireBreathPos.position.z),
                myEnemy.moves[0].transform.rotation);
            if (isGrounded)
            {
                currentAttackEffect.transform.position = new Vector3(fireBreathPos.position.x,
                    fireBreathPos.position.y - offset.y, fireBreathPos.position.z);
                currentAttackEffect.transform.rotation =
                    Quaternion.Euler(fireBreathPos.rotation.x, 180, fireBreathPos.rotation.z);
            }

            else
            {
                currentAttackEffect.transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            currentAttackEffect.SetUser(myClass);
            isFire = true;
        }
        
        public void FlameAttackOff()
        {
            if (currentAttackEffect != null && !isFire)
                return;
            Destroy(currentAttackEffect.gameObject);
            
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(fireBreathPos.position, AttackRange);
        }
    }
}