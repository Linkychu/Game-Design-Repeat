using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public List<Transform> players = new List<Transform>();
        public Transform[] slots;

        public List<Transform> Allies = new List<Transform>();

       public ChosenChosenCharacters dataModels;


        public bool ableToSpawnAllies;

        public Transform allyObject;
        

        public List<Color> playerSpriteColors = new List<Color>();
        public List<Transform> AliveAllies = new List<Transform>();
        public static PlayerManager instance { get; private set; }

        public Transform spawnPos;
        private Transform parent;
        public LayerMask groundMask;
        private Vector3[] slotPos = new Vector3[3];
        private GameObject model;

        private int count;
        List<CharacterData> models = new List<CharacterData>();
        
        private List<GameObject> SpawnedModels = new List<GameObject>();
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
            parent = GameObject.FindWithTag("Ally1").transform;
            count = 0;
            foreach (Transform child in parent)
            {
                Allies.Add(child);
            }
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


        public void SpawnNavmesh()
        {
            
           
        }
        public void SpawnAllies()
        {
            allyObject.position = players[0].position;
            
            models.Add(dataModels.chosenCharacter1);
            models.Add(dataModels.chosenCharacter2);
            models.Add(dataModels.chosenCharacter3);
            for (int i = 0; i < Allies.Count; i++)
            {
                models[i].Spawn(new Vector3(Allies[i].position.x, -2f, Allies[i].position.z), new Vector3(Allies[i].position.x, players[0].transform.position.y, Allies[i].position.z), Allies[i].transform, i);
               

                if (players.Count > Allies.Count)
                {
                    Destroy(models[i].model.gameObject);
                }

                else
                {
                    players.Add(models[i].model.gameObject.transform);
                    

                }
                
               
            }



            AliveAllies = players;
            playerSpriteColors.Add(Color.red);
            playerSpriteColors.Add(Color.blue);
            playerSpriteColors.Add(Color.yellow);
            playerSpriteColors.Add(Color.green);


        }

        public void ResetPositions()
        {
            Allies.Clear();
            parent = GameObject.FindWithTag("Ally1").transform;
            
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent(out AllyClass ally))
                {
                    Destroy(ally.gameObject);
                }
               
                Allies.Add(child);
            }
            transform.position = Vector3.zero;

           
          
            
            StartCoroutine(Place());
        }

        IEnumerator Place()
        {

            players[0].position = Vector3.zero;
            yield return null;
            
            if (spawnPos == null)
            {
                spawnPos = GameObject.FindWithTag("SpawnPos").transform;
            }
            else
            {
                Debug.Log(spawnPos.position);
                players[0].position = spawnPos.transform.position;
                Debug.Log("spawn");
            }
           
            yield return new WaitUntil(() => players[0].GetComponent<CharacterController>().isGrounded);
                //allyObject.position = new Vector3(players[0].position.x, players[0].position.y, players[0].position.z);

            for (int i = 0; i < slots.Length; i++)
            {
                    models[i].Spawn(Allies[i].position,
                        new Vector3(Allies[i].position.x, Allies[i].position.y + 0.1f, Allies[i].position.z), Allies[i],
                        i);
                    if ((!AliveAllies.Contains(models[i].model.transform)))
                    {
                        Destroy(models[i].model.gameObject);
                    }

                    
            }
            
        }

    }
}