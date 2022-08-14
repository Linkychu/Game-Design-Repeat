using System;
using UnityEngine;
using UnityEngine.VFX;

public enum MoveType
{
    Physical,
    Special
};
[CreateAssetMenu(fileName = "Move", menuName = "Data/Moves") ]
    public class Moves : ScriptableObject
    {
        public int baseDamage;
        public MoveType type;
        public Affinities affinity;
        public int manaCost;
        public GameObject model;
        public Vector3 rotationOffset;
        public float range;
        public float lifetime;
        public CharacterClass user;
        
        /*public void Spawn(Vector3 position, Transform transform)
        {
            GameObject clone = Instantiate(model, position, Quaternion.Euler(rotationOffset), transform) as GameObject;
        }*/

        
        
        
        
        
    }

