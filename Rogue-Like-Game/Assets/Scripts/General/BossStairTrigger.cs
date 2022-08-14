using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace General
{
    public class BossStairTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManagerScript.instance.LoadBossScene("BattleScene");
            }
        }
    }
}