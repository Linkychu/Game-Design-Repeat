using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 0)
        {
            transform.parent = target;
        }
    }

    // Update is called once per frame
    
}
