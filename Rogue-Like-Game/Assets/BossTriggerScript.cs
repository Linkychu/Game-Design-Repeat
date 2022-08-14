using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerScript : MonoBehaviour
{
    public EventHandler OnPlayerEnterTrigger;
    private void OnTriggerEnter(Collider other)
    {
        PlayerClass player = other.GetComponent<PlayerClass>();

        if (player != null)
        {
            OnPlayerEnterTrigger.Invoke(this, EventArgs.Empty);
        }
    }
}
