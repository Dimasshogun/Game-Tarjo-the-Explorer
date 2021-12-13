using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Character
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private GameObject gameplayUI;
        private Rigidbody _rigidbody;
        private Animator _modelAnimator;
    
        [SerializeField] private float baseSpeed = 10f;
        private float _speed;
        private Transform _mainCameraTransform;
        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private MovementControls _controls;

        private void Awake()
        {
            _controls = new MovementControls();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _modelAnimator = GetComponentInChildren<Animator>();
            _mainCameraTransform = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            
            if (gameplayUI == null)
            {
                GameObject.Find("GameplayUI");
            }
            _speed = baseSpeed;
        }
        private void FixedUpdate()
        {
            var inputDirection = _controls.Player.Movement.ReadValue<Vector2>();
            if (inputDirection.x != 0f || inputDirection.y != 0f)
            {
                Move(inputDirection);
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
        }
        
        public void Move(Vector2 inputDirection)
        {
            var right = _mainCameraTransform.right;
            var forward = _mainCameraTransform.forward;
            
            right.y = 0f;
            forward.y = 0f;
            
            right.Normalize();
            forward.Normalize();
            
            var direction = forward * inputDirection.y + right * inputDirection.x;
            
            Vector3 target = transform.position + direction * _speed * Time.deltaTime;
            _rigidbody.MovePosition(target);
        }


        private void OnEnable()
        {
            _controls.Player.Enable();
            gameplayUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            gameplayUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
