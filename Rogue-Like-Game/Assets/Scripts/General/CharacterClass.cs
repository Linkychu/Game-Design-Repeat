using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.VFX;

public interface IDamageable
{
    void Damage(int damage);
}

public enum StatBoost
{
    None,
    Attack,
    Defence,
    Special
}
public interface IBoostable
{
    public void BoostStats(int amount, StatBoost boosts);
}
[System.Serializable]
public class Attributes
{
    public float A_Modifier;
    public float D_Modifier;
    public float S_Modifier;
        
}

public class CharacterClass : MonoBehaviour, IDamageable
{
    
    
    public CharacterBase charBase;
    private StatBoost boosts;

    public bool isSetLevel;

    

    public CharacterValues values;

    public Slider HealthBar;
    public Gradient gradient;
    public Image fill;
        
    public Slider manaSlider;
    public Gradient manaGradient;
    public Image manaFill;

    public Attributes attributes = new Attributes();
    public bool isPlayer;
    public TextMeshProUGUI levelText;
    public Transform healthBar;
    [SerializeField] private int minLevel;
    [SerializeField] private int maxLevel;
    public GameObject levelUpEffect;
    public Level level;
    public GameObject boostEffect;
    private GameObject boost;
    [HideInInspector] public bool isStatsBoosted;

    
    

    void Start()
    {

      

        
       
        
        if (!isPlayer && !isSetLevel)
        {
            values.myStats.level = RNG.RngCallRange(minLevel, maxLevel);
           
        }
        
        level = new Level(values.myStats.level, OnLevelUp);

       
        ModifyStat(attributes, 1, 1, 1);
        SetUpCharacter();
        //OnLevelUp();
    }
    
    

    public void OnLevelUp()
    {
        
        int oldEXP = level.experience;
        int newexp = level.GetXPforLevel(values.myStats.level);
        level.experience = 0;
        level.experience = Mathf.Abs(oldEXP - newexp);
        if (charBase.myStatus != Class.Enemy)
        {
            var asset = Instantiate(levelUpEffect, transform.position, Quaternion.identity);
            Destroy(asset, 1);
        }

        IncreaseStats();
        

       
    }
    
    
    
    void IncreaseStats()
    {
        values.myStats.MaxHP = LevelUpStats(charBase.BaseHP, values.myStats.level, 10);
        values.myStats.maxMana = LevelUpStats(charBase.BaseManaAmount, values.myStats.level, 10);
        values.myStats.Attack = LevelUpStats(charBase.BaseAttack, values.myStats.level, 5);
        values.myStats.Defense = LevelUpStats(charBase.BaseDefense, values.myStats.level, 5);
        values.myStats.Special = LevelUpStats(charBase.BaseSpecial, values.myStats.level, 5);
        values.myStats.Speed = LevelUpStats(charBase.BaseSpeed, values.myStats.level, 5);
        Heal(values.myStats.MaxHP);
        HealMana(values.myStats.maxMana);
        values.myStats.level = level.currentLevel;

        if(gameObject.CompareTag("Enemy"))
        {
            levelText.text = $" Name: {charBase.Name} \n Type: {charBase.type.affinity.ToString()} \n Level: {values.myStats.level}";
        }
        
        else if (gameObject.CompareTag("Ally"))
        {
            levelText.text = $" Name: {charBase.Name} \n Level: {values.myStats.level}";
        }

        else
        {
            levelText.text = $"Level : {values.myStats.level}";
        }
        
    }
    void SetUpCharacter()
    {
        
       IncreaseStats();
      

    }


    public void HealMana(int amount)
    {
        values.myStats.currentMana += amount;
        if (manaSlider != null)
        {
            values.myStats.currentMana = Mathf.Clamp(values.myStats.currentMana, 0, values.myStats.maxMana);
            manaSlider.value = values.myStats.currentMana;
            manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
        }
    }
    
    public void Heal(int health)
    {
        values.myStats.currentHP += health;
        values.myStats.currentHP = Mathf.Clamp(values.myStats.currentHP, 0, values.myStats.MaxHP);
        HealthBar.value = values.myStats.currentHP;
        fill.color = gradient.Evaluate(HealthBar.normalizedValue);
    }
    
    public void UseMana(int amount)
    {
        values.myStats.currentMana -= amount;
        Debug.Log(amount);
        if (manaSlider != null)
        {
            values.myStats.currentMana = Mathf.Clamp(values.myStats.currentMana, 0, values.myStats.maxMana);
            manaSlider.value = (float) values.myStats.currentMana / values.myStats.maxMana;
            manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
        }
    }
    
    
    public void Damage(int damage)
    {
        values.myStats.currentHP -= damage;
        Debug.Log(damage);
        values.myStats.currentHP = Mathf.Clamp(values.myStats.currentHP, 0, values.myStats.MaxHP);
        HealthBar.value = (float)values.myStats.currentHP/values.myStats.MaxHP;
        fill.color = gradient.Evaluate(HealthBar.normalizedValue);
        if (values.myStats.currentHP <= 0)
        {
            Death();
        }
        
    }

    int LevelUpStats(int baseStat, int currentLevel, int rate)
    {
        int statEXP = Mathf.FloorToInt((RNG.RngCallRange(1, 255)) );
        statEXP /= 4;
        int result = Mathf.FloorToInt((((baseStat) * 2 + statEXP * currentLevel) / 25) + currentLevel + rate);
        return result;
    }
    
  
    private static void ModifyStat(Attributes Stats, float amountX, float amountY, float amountZ)
    {
        Stats.A_Modifier = amountX;
        Stats.D_Modifier = amountY;
        Stats.S_Modifier = amountZ;
    }



    public virtual void Death()
    {
        
    }
    private void OnEnable()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!isPlayer)
        {
            healthBar.LookAt(Camera.main.transform, transform.up);
        }

        
        if (isStatsBoosted)
        {
            
            if (boost == null)
            {
                var boosted = Instantiate(boostEffect, transform.position, Quaternion.identity);
                boosted.transform.position = transform.position;
                boost = boosted;
            }

            boost.transform.position = transform.position;
        }

        else
        {
            if (boost != null)
            {
                Destroy(boost.gameObject);
                boost = null;
            }
        }
    }
}
