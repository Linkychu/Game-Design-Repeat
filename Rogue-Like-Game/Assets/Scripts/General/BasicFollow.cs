using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BasicFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public void Start()
    {
        
    }


    public void LateUpdate()
    {
        Vector3 targetPos = target.position - offset;
        Vector3 npcPos = transform.position;

        Vector3 delta = (targetPos - npcPos).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100f);
        transform.position = targetPos;

    }
}