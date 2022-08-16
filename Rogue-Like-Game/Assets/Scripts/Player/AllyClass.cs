using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Player;
using UnityEngine;

public class AllyClass : CharacterClass, IDamageable, IBoostable
{
   public int Level;
    private void Awake()
    {
       
    }

    public void SetUp()
    {
        values = Instantiate(values);
        ModifyStat(attributes, 1, 1, 1);
        values.myStats.level = Level;
        
        charBase = Instantiate(charBase);
        level = new Level(values.myStats.level, OnLevelUp);

       
       
        SetUpCharacter();
    }

    
    public override void Death()
    {
        if (PlayerManager.instance.AliveAllies.Contains(transform))
        {
            PlayerManager.instance.AliveAllies.Remove(transform);
            gameObject.SetActive(false);
            

        }

        else
        {
            return;
            
        }
    }

    private void OnDisable()
    {
        // DialogueManager.instance.DisplayDialogue(charBase.DeathMessage);
        //Destroy(gameObject);
    }

    public void BoostStats(int amount, StatBoost boost)
    {
        isStatsBoosted = true;
        StartCoroutine(BoostStatsTime(amount, boost));

    }

    IEnumerator BoostStatsTime(int amount, StatBoost boost)
    {
        Attributes boostedAttribute = null;
        switch (boost)
        {
            case StatBoost.Attack:
                attributes.A_Modifier = amount;
                boostedAttribute = attributes;
                isStatsBoosted = true;
                break;
            case StatBoost.Defence:
                attributes.D_Modifier = amount;
                boostedAttribute = attributes;
                isStatsBoosted = true;
                break;
            case StatBoost.Special:
                isStatsBoosted = true;
                boostedAttribute = attributes;
                attributes.S_Modifier = amount;
                break;
            default:
                isStatsBoosted = false;
                break;
        }

       
        yield return new WaitForSeconds(10);
        if (boostedAttribute != null)
        {
            boostedAttribute.A_Modifier = 1;
            boostedAttribute.D_Modifier = 1;
            boostedAttribute.S_Modifier = 1;
        }

        isStatsBoosted = false;
    }
}
