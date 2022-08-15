using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public Moves move;
    public bool shouldMove;
    public bool isDynamicRanged;

    private Rigidbody rb;

    private SphereCollider _collider;

    private CharacterClass _class;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();

        if (!isDynamicRanged)
        {
            _collider.radius = move.range;
        }

        Destroy(this.gameObject, move.lifetime);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMove)
            return;
        transform.Translate(0,0,0);
    }
    
    public void SetUser(CharacterClass @class)  
    {
        _class = @class;
    }

    private void OnTriggerEnter(Collider other)
    {


        CharacterClass enemyClass = other.GetComponent<CharacterClass>();
        if(enemyClass == null)
            return;
        if (_class.charBase.myStatus == enemyClass.charBase.myStatus)
            return;
        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable.Damage(DamageManager.instance.DamageCalculator(move.baseDamage, _class.values.myStats.Attack, enemyClass.values.myStats.MaxHP,
            enemyClass.values.myStats.Defense, _class.values.myStats.Special, enemyClass.values.myStats.Special, _class.attributes.A_Modifier, _class.attributes.D_Modifier, _class.attributes.S_Modifier, move.type,
            move.affinity, enemyClass.charBase.type, _class.values.myStats.level, enemyClass.values.myStats.level, enemyClass.attributes.D_Modifier, enemyClass.attributes.S_Modifier));
       
    }
}
