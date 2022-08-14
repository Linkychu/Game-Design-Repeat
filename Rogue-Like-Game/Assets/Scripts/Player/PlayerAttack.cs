using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        
        private bool canAttack = true;
        public float CoolDownDuration = 1f;

        private CharacterClass _class;

        public Vector3 offset = new Vector3(1 , 1, 1);

        public AttackEffect slash;

        private void Awake()
        {
            
        }
        

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _class = GetComponent<CharacterClass>();    

        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Invoke("Attack", 0.1f);
            }
        }

        public void SlashOn()
        {
               var slashObj = Instantiate(slash, transform.position + offset + transform.forward, transform.rotation);
               slashObj.SetUser(_class);
        }

        public void SlashOff()
        {
            //Destroy(Slashes[0]);
            
        }

        void Attack()
        {
            if(canAttack == false)
                return;
            
            _animator.SetTrigger("Attack");

            StartCoroutine(CoolDownTimer());

        }

        void CheckForTarget()
        {
            
        }

        IEnumerator CoolDownTimer()
        {
            canAttack = false;
            yield return new WaitForSeconds(CoolDownDuration);
            canAttack = true;
        }
    }
}