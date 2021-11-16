using System;
using UnityEditor;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Animator _modelAnimator;
    
        [SerializeField] private float baseSpeed = 10f;
        private float _speed;
        private Transform _mainCameraTransform;
        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _speed = baseSpeed;
            _rigidbody = GetComponent<Rigidbody>();
            _modelAnimator = GetComponentInChildren<Animator>();
            _mainCameraTransform = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update()
        {
            if (Input.GetAxisRaw("Horizontal") != 0f ||Input.GetAxisRaw("Vertical") != 0f)
            {
                var xInput = Input.GetAxis("Horizontal");
                var yInput = Input.GetAxis("Vertical");
                Move(xInput, yInput);
                _modelAnimator.SetFloat(XDirection, xInput);
                _modelAnimator.SetFloat(YDirection, yInput);
                _modelAnimator.SetBool(IsWalking, true);
            }
            else
            {
                if (_modelAnimator.GetBool(IsWalking))
                {
                    _modelAnimator.SetBool(IsWalking, false);
                }
            }
            
        }
        
        private void Move(float x, float y)
        {
            var right = _mainCameraTransform.right;
            var forward = _mainCameraTransform.forward;

            right.y = 0f;
            forward.y = 0f;
        
            right.Normalize();
            forward.Normalize();

            var direction = forward * y + right * x;
        
            Vector3 target = transform.position + direction * _speed * Time.deltaTime;
            _rigidbody.MovePosition(target);
        }
        
    }
}
