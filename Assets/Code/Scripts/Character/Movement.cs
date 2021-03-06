using System;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Animator _modelAnimator;
        private AudioSource _audioSource;
    
        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _modelAnimator = GetComponentInChildren<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void Move(Vector3 target, Vector2 inputDirection)
        {
            AnimateMove(inputDirection);
            _rigidbody.MovePosition(target);
        }

        private void AnimateMove(Vector2 inputDirection)
        {
            if (inputDirection.x != 0f || inputDirection.y != 0f)
            {
                _modelAnimator.SetFloat(XDirection, inputDirection.x);
                _modelAnimator.SetFloat(YDirection, inputDirection.y);
                _modelAnimator.SetBool(IsWalking, true);
                if(!_audioSource.isPlaying){
                    _audioSource.Play();
                }
            }
            else
            {
                if (_modelAnimator.GetBool(IsWalking))
                {
                    _modelAnimator.SetBool(IsWalking, false);
                }
                if(_audioSource.isPlaying){
                    _audioSource.Stop();
                }
            }
        }

        private void OnDisable()
        {
            AnimateMove(Vector2.zero);
        }
    }
}
