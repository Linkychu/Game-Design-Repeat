using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.AI;


public class EnemyRandomPathFinding : MonoBehaviour
{
    public enum enemyState
    {
        patrol,
        chase,
        attack
    }
    private Animator _animator;

    private NavMeshAgent Agent;

    public float lookRadius = 10f;
    

    public float radius = 2;
    public float Range;

    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");

    private bool canReachPath;

    public LayerMask playerMask;

    public enemyState _enemyState;

    public Transform target;
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public Vector3 offset;
    

    // Start is called before the first frame update
    void Start()
    {
        
        _animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
       
    }


    private void Update()
    {
        switch (_enemyState)
        {
            case enemyState.patrol:
                Patrol();
                break;
            case enemyState.attack:
                Attack();
                break;
            case enemyState.chase:
                Chase();
                break;
        }
        
        
    }

    // Update is called once per frame


    public bool IsPlayerVisible()
    {
        Transform closestPlayer = null;
        Collider[] players = new Collider[4];
        players = Physics.OverlapSphere(transform.position, lookRadius, playerMask);
        if (players.Length > 0)
        {
            float distance = 0;
            foreach (var player in players)
            {
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist > distance)
                {
                    distance = dist;
                    closestPlayer = player.transform;
                }

                
            }

            target = closestPlayer;
            return true;
        }

        closestPlayer = null;
        return false;
    }

    public bool IsPlayerInRange()
    {
        if (target == null)
            return true;
        return Vector3.Distance(transform.position, target.position) < Range;
    }

    public void SwitchState (enemyState state)
    {
        _enemyState = state;
    }

    void Attack()
    {
        transform.LookAt(target);
        _animator.SetTrigger(Attack1);
    }

    public void Idle()
    {
        _animator.SetBool(IsPatrolling, true);
        ResetAnimationBools();
    }


    void Chase()
    {
        
        

        if (!IsPlayerInRange())
        {
            _animator.SetBool(IsPatrolling, true);
            Agent.destination = target.position;
        }
        
        else if (IsPlayerInRange())
        {
            _animator.SetBool(IsPatrolling, false);
        }
        
       
        
       

      


    }
    public void Patrol()
    {
       // DistanceCheck();
        if (!Agent.hasPath)
        {
            ResetAnimationBools();
            _animator.SetBool(IsPatrolling, true);
            Agent.SetDestination(GetRandomPoint());
            //Agent.SetDestination(GetRandomPoint(transform, radius));
        }

        
    }

    Vector3 GetRandomPoint(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (RandomPoint(point == null ? transform.position : point.position, radius == 0 ? Range : radius, out _point))
        {
           
           // Debug.DrawRay(_point, Vector3.up, Color.green, 1);
            return _point;
        }


        return point == null ? Vector3.zero : point.position;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            
           
            NavMeshHit hit;
            
            NavMeshPath navMeshPath = new NavMeshPath();
            Agent.CalculatePath(randomPoint, navMeshPath);
            canReachPath = navMeshPath.status == NavMeshPathStatus.PathComplete;

            if (!canReachPath) continue;
            if (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) continue;
            result = hit.position;
            /*NavMeshPath path = new NavMeshPath();
                    Agent.CalculatePath(result, path);
                    canReachPath = path.status == NavMeshPathStatus.PathComplete;*/
            return true;

        }
        
        result = Vector3.zero;
       // enemyState = State.Idle;
        return canReachPath;
    }

    

    void FaceTarget()
    {
        
    }

    void ResetAnimationBools()
    {
        foreach (AnimatorControllerParameter parameter in _animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
               _animator.SetBool(parameter.name, false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    
#endif
}

