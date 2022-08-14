using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class BossOverlapCheck : MonoBehaviour
    {
        private Collider _collider;
        public LayerMask bossMask = 9;
        
        private void Start()
        {
            _collider = GetComponent<Collider>();

            if (_collider is MeshCollider)
            {
                MeshCollider collider = GetComponent<MeshCollider>();
                collider.convex = true;
            }
            StartCoroutine(CheckForBoss());
        }

        private void Update()
        {
            Collider[] boss = new Collider[1];

            int amount =
                Physics.OverlapBoxNonAlloc(transform.position, Vector3.up, boss, Quaternion.identity, bossMask);

            if (amount > 0)
            {
                Destroy(gameObject);
            }

        }


        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator CheckForBoss()
        {
            yield return new WaitForFixedUpdate();

           Collider[] boss = new Collider[1];

           int amount =
               Physics.OverlapBoxNonAlloc(transform.position, Vector3.up, boss, Quaternion.identity, bossMask);

           if (amount > 0)
           {
               Destroy(gameObject);
           }

           else
           {
               if (_collider is MeshCollider)
               {
                   MeshCollider collider = GetComponent<MeshCollider>();
                   collider.convex = false;
               }
               
           }
        }
    }