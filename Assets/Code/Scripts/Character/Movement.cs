using UnityEngine;

namespace Code.Scripts.Character
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Animator _modelAnimator;
    
        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _modelAnimator = GetComponentInChildren<Animator>();
        }
        
        public void Move(Vector3 target, Vector2 inputDirection)
        {
            Debug.Log(inputDirection);
            if (inputDirection.x != 0f || inputDirection.y != 0f)
            {
                _modelAnimator.SetFloat(XDirection, inputDirection.x);
                _modelAnimator.SetFloat(YDirection, inputDirection.y);
                _modelAnimator.SetBool(IsWalking, true);
            }
            else
            {
                if (_modelAnimator.GetBool(IsWalking))
                {
                    _modelAnimator.SetBool(IsWalking, false);
                }
            }
            
            _rigidbody.MovePosition(target);
        }
    }
}
