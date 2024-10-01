
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
        Animator _anim;

        bool canWalk=true;
        
        [Header("EnemyStats")]
        public bool isLive=true; 
        [SerializeField] private float _health=10; //default 10
        
        //
        void Components()
        {
            _enemyAI=GetComponent<EnemyAI>();
            _anim=GetComponent<Animator>();
        }
        private void Awake() 
        {
            Components();
        }
        void Update()
        {
            if (!isLive)
            {
                _enemyAI.StopMoving();
            }
            if (isLive)
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
            isLive=false;
            
            _enemyAI.Die();
            Destroy(gameObject,_bodyDestroyTimer);
            IsLive();
            
        }
        private void IsLive()
        {
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
            _anim.SetBool("isLive",isLive);
        }


        public void Interaction()
        {
            Debug.Log(gameObject.name);
        }

    }
}
