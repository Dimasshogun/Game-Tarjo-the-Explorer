using System;
using UnityEditor;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Transform _modelTransform;
    
        [SerializeField] private float baseSpeed = 10f;
        private float _speed;
        private Transform _mainCameraTransform;

        private void Start()
        {
            _speed = baseSpeed;
            _rigidbody = GetComponent<Rigidbody>();
            _modelTransform = GetComponentInChildren<SpriteRenderer>().transform;
            _mainCameraTransform = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update()
        {
            if (Input.GetAxis("Horizontal") != 0f ||Input.GetAxis("Vertical") != 0f)
            {
                Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            
            TurnToCamera();
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
        
        private void TurnToCamera()
        {
            var cameraPos = _mainCameraTransform.transform.position;

            cameraPos.y = transform.position.y;
        
            _modelTransform.LookAt(cameraPos);
        }
    }
}
