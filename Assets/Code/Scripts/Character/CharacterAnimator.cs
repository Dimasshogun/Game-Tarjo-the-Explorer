using System;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public string idleState = "Idle";
        public string speakState = "Speak";

        private void Start()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            
            animator.Play(idleState);
        }

        public void AnimateCharacter(string type, bool updateIdle = false)
        {
            switch (type)
            {
                case "Speak":
                    animator.Play("Speak");
                    break;
                case "Cry":
                    animator.Play("Cry");
                    break;
                case "Happy":
                    animator.Play("Happy");
                    break;
                case "Wave":
                    animator.Play("Wave");
                    break;
                default:
                    animator.Play("Idle");
                    break;
            }

            if (updateIdle)
            {
                idleState = type;
            }
        }
    }
}