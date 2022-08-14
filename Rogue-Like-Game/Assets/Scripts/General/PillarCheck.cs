using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PillarCheck : MonoBehaviour
{
    public GameObject pillar;

    public float size = 3;

    public LayerMask pillarMask;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        Collider[] pillars = new Collider[1];
        int amount = Physics.OverlapBoxNonAlloc(transform.position, Vector3.up * size, pillars, Quaternion.identity, pillarMask);

        if (amount == 0)
        {
            GameObject pillarIns = Instantiate(pillar, transform.position, transform.rotation, transform);
            pillarIns.transform.localScale = Vector3.one;
        }
    }
}
 