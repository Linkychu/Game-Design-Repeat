using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public List<Transform> players = new List<Transform>();
        public Transform[] slots;

        public Transform[] Allies;

        public GameObject[] models;


        public bool ableToSpawnAllies;

        public List<Color> playerSpriteColors = new List<Color>();
        public List<Transform> AliveAllies = new List<Transform>();
        public static PlayerManager instance { get; private set; }


        private Vector3[] slotPos = new Vector3[3];

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
            players.Add(GameObject.FindObjectOfType<PlayerClass>().transform);
            //players[0].gameObject.SetActive(false);
            //SpawnAllies();
        }

       

        private void Start()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slotPos[i] = slots[i].position;
            }
        }

        public void SpawnAllies()
        {
            for (int i = 0; i < Allies.Length; i++)
            {
                var model = Instantiate(models[i], Allies[i].position, Quaternion.identity, Allies[i]);
                model.GetComponent<PartyFollow>().index = i;

                if (players.Count > Allies.Length)
                {
                    Destroy(model);
                }

                else
                {
                    players.Add(model.transform);
                    AliveAllies.Add(model.transform);

                }
            }

          

            playerSpriteColors.Add(Color.red);
            playerSpriteColors.Add(Color.blue);
            playerSpriteColors.Add(Color.yellow);
            playerSpriteColors.Add(Color.green);


        }

        public void ResetPositions()
        {
            
           players[0].position = Vector3.zero;
         

            foreach (var ally in Allies)
            {
                ally.GetComponentInChildren<NavMeshAgent>().isStopped = true;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                slotPos[i] = slots[i].position;
            }

            for (var index = 0; index < Allies.Length; index++)
            {
                Transform ally = Allies[index];
                var agent = ally.GetComponentInChildren<NavMeshAgent>();
                agent.enabled = false;
                Transform AllyParent = Allies[index].transform;
                AllyParent.position = new Vector3(0, -11.8f, 0);
                agent.enabled = true;
                agent.Warp(new Vector3(0, agent.transform.position.y + 0.1f, 0));
                agent.isStopped = false;

            }

           
            
           
            
        }
    }
}