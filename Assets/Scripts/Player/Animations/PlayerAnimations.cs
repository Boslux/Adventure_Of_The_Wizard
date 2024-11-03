using UnityEngine;

namespace NRPG.Player.Animations
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
