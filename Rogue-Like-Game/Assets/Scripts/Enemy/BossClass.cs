using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossClass : CharacterClass
{
    public int Level;
    public delegate void BossDelegate();

    public BossDelegate OnBossDeath;
    
    // Start is called before the first frame update
    private void Awake()
    {
        charBase = (CharacterBase) Instantiate(charBase);
        values.myStats.level = Level;
    }

    public override void Death()
    {
        OnBossDeath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
