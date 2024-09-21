using System.Collections;
using System.Collections.Generic;
using NRPG.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace NRPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [Header("NavMeshAgent")]
        NavMeshAgent _navMeshAgent;
        [Header("SpriteRenderer")]
        SpriteRenderer _spriteRenderer;
        private bool isFacingRight = false; //default olarak karakter sola bakıyor varsayalım
        [Header("Animations")]
        Animator _anim;


        private void Awake() 
        {
            Components(); 
            ConfigureNavMeshAgent(); 
        }
        void Components()
        {
            _anim=GetComponent<Animator>();
            _navMeshAgent=GetComponent<NavMeshAgent>();
            _spriteRenderer=GetComponent<SpriteRenderer>();
        }
        void ConfigureNavMeshAgent()
        {
            //karakterin 3d objelere uygun dönüşünü durdurdu
            _navMeshAgent.updateRotation=false;
            _navMeshAgent.updateUpAxis=false;
        }

        public void MoveToTarget(Vector2 target)
        {
            //hedefe doğru git
            _navMeshAgent.SetDestination(target);
            //karakteri döndür
            FlipPlayer(target);
            //yürüme animasyonunu çalıştır
            
        }
#region Walk Animation
        void WalkAnimation(bool canWalk)
        {
            _anim.SetBool("isWalking",canWalk);
        }
        public void IsWalking()
        {
            bool canWalk;
            if (_navMeshAgent.velocity.magnitude!=0)canWalk=true;
            else canWalk=false;

            WalkAnimation(canWalk); 
        }
#endregion
#region Face Right
        void FlipPlayer(Vector2 target)
        {
             bool shouldFaceRight = target.x > transform.position.x;

            // Yön değişikliği gerektiğinde flip işlemi yap
            if (shouldFaceRight != isFacingRight)
            {
                isFacingRight = shouldFaceRight;
                _spriteRenderer.flipX = !isFacingRight; // Sağdayken flipX false, soldayken true
            }
        }
#endregion
    }
}
