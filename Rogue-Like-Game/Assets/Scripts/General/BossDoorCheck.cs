using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorCheck : MonoBehaviour
{
    private Collider _collider;

    public LayerMask Checks;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();

        if (_collider is MeshCollider)
        {
            MeshCollider collider = GetComponent<MeshCollider>();
            collider.convex = true;
        }
        StartCoroutine(CheckForBoss());
    }

    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator CheckForBoss()
    {
        yield return new WaitForFixedUpdate();

        Collider[] boss = new Collider[1];

        int amount =
            Physics.OverlapBoxNonAlloc(transform.position, Vector3.one, boss, Quaternion.identity, Checks, QueryTriggerInteraction.Collide);

        if (amount > 0)
        {
            Destroy(boss[0].gameObject);
            
        }

        if (_collider is MeshCollider)
        {
            MeshCollider collider = GetComponent<MeshCollider>();
            collider.convex = false;
        }
        else
        {
            
               
        }
    }
}
