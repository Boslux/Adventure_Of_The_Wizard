using System;
using System.Collections;
using System.Collections.Generic;
using NRPG.Controller;
using NRPG.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace NRPG.Core
{
    public class EnemyController : MonoBehaviour, IInteraction
    {
        [Header("Components")]
        EnemyAI _enemyAI;
        EnemyDropSystem _dropSystem;
        Animator _anim;

        bool canWalk=true;
        
        [Header("EnemyStats")]
        public bool _isLive=true; 
        [SerializeField] private float _health=10; //default 10
        
        //
        void Components()
        {
            _enemyAI=GetComponent<EnemyAI>();
            _dropSystem=GetComponent<EnemyDropSystem>();
            _anim=GetComponent<Animator>();
        }
        private void Awake() 
        {
            Components();
        }
        void Update()
        {
            if (!canWalk)
            {
                _enemyAI.StopMoving();
            }
            if (canWalk)
            {
                _enemyAI.EnemyBehavior();
            }
        }
        
        public void TakeDamage(float damage)
        {
            
            _health-=damage;
            
            _anim.SetTrigger("hurt");
            if (_health <= 0) 
            {
                Died();   
            }
        
        }
        private void Died()
        {
            float _bodyDestroyTimer=10;

            _health=0;
            _anim.SetBool("isLive",false);
            Destroy(gameObject,_bodyDestroyTimer);
            _dropSystem.ItemDrop();
            gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
            
        }

        public void Interaction()
        {
            Debug.Log(gameObject.name);
        }
        private void OnTriggerEnter2D(Collider2D cls) 
        {
            if(cls.gameObject.CompareTag("Skill"))
            {
                float damage=Resources.Load<CharacterStats>("Character Stats").attackPower;
                TakeDamage(damage);
                Destroy(cls.gameObject);
            }
        }
    }
}
