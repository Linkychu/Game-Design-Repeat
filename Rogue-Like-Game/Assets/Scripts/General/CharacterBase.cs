using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Class
{
   Enemy,
   Ally
}



[CreateAssetMenu(fileName = "CharacterBase", menuName = "Data/Character")]
public class CharacterBase : ScriptableObject
{
   public string Name;
   public int BaseHP;
   public int BaseAttack;
   public int BaseDefense;
   public int BaseSpecial;
   public int BaseSpeed;
   public int BaseManaAmount;
   private int level;
   public Affinities type;
   public Class myStatus;
   public TextAsset DeathMessage;

}
