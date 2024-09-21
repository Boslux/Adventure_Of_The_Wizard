using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRPG.Animations
{
    public class PlayerAnimations : MonoBehaviour
    {
        Animator _anim;

        private void Awake() 
        {
            _anim = GetComponent<Animator>();    
        }

        public void PlayWalkAnimation(bool isWalking)
        {
            _anim.SetBool("isWalking", isWalking);
        }
    }
}
