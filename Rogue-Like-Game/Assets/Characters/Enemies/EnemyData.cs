using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject enemy;
    public float chanceToSpawn;

    private Vector3 initialPos;
    private Vector3 finalPos;
    private Transform Parent;
    public void Spawn(float value, Vector3 InitialPos, Vector3 FinalPos, Transform parent)
    {
        /*initialPos = InitialPos;
        finalPos = FinalPos;
        Parent = parent;*/
        if (value < chanceToSpawn)
        {
            var Enemy = Instantiate(enemy, InitialPos, Quaternion.identity, parent);
            NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>();
            agent.Warp(FinalPos);
        }

        else
        {
            int nextValue = RNG.RngCallRange(0, 100);
            Spawn(nextValue, InitialPos, FinalPos, parent);
            return;
        }
    }
}
