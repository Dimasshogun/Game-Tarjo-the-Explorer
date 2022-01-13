using System;
using UnityEngine;

namespace Code.Scripts.Minigame
{
    public class MinigameDrag : MonoBehaviour, IDrag
    {
        public string answerValue;
        public MinigameDropTarget target;

        private Rigidbody _rb;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        private Camera _mainCamera;
        private MinigameControls _controls;

        private void OnDisable()
        {
            _controls.Disable();
        }
        private void Awake()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _controls = new MinigameControls();
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        public void OnDragStart()
        {
            _rb.isKinematic = false;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            
            target = null;
        }

        public void OnDrag()
        {
            var ray = _mainCamera.ScreenPointToRay(_controls.Player.TouchPoint.ReadValue<Vector2>());
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Minigame")))
            {
                return;
            }

            if (hit.collider == null || hit.collider.gameObject.GetComponent<MinigameDropTarget>() == null)
            {
                return;
            }

            var objectTransform = transform;
            var targetTransform = hit.collider.transform;
            objectTransform.position = targetTransform.position;
            objectTransform.rotation = targetTransform.rotation;
        }
        
        public void OnDragEnd()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (target == null)
            {
                transform.position = _initialPosition;
                transform.rotation = _initialRotation;
                _rb.isKinematic = true;
                return;
            }
            
            if (target.correctAnswer == answerValue)
            {
                target.isCorrect = true;
                transform.position = target.transform.position;
                transform.rotation = target.transform.rotation;
                target.enabled = false;
                MinigameManager.Instance.PlayCorrect();
                _rb.isKinematic = true;
                return;
            }
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            MinigameManager.Instance.PlayIncorrect();
            target = null;
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.None;
        }
    }
}