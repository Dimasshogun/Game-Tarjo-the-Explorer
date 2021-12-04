using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Minigame
{
    public class DragAndDrop3D : MonoBehaviour
    {
        [SerializeField] private InputAction mouseClick;
        [SerializeField] private float mouseSpeedPhysics = 10.0f;
        [SerializeField] private float mouseSpeed = 0.1f;
        
        private Camera _mainCamera;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            mouseClick.Enable();
            mouseClick.performed += MousePressed;
        }

        private void OnDisable()
        {
            mouseClick.Disable();
            mouseClick.performed -= MousePressed;
        }

        private void MousePressed(InputAction.CallbackContext context)
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable"))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

        public virtual IEnumerator DragUpdate(GameObject draggedObject)
        {
            float initialDistance = Vector3.Distance(draggedObject.transform.position, _mainCamera.transform.position);
            draggedObject.TryGetComponent<Rigidbody>(out var rb);
            draggedObject.TryGetComponent<IDrag>(out var iDrag);
            
            while (mouseClick.ReadValue<float>() != 0)
            {
                var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (rb != null)
                {
                    var direction = ray.GetPoint(initialDistance) - draggedObject.transform.position;
                    rb.velocity = direction * mouseSpeedPhysics;
                    yield return _waitForFixedUpdate;
                }
                else
                {
                    draggedObject.transform.position = Vector3.SmoothDamp(draggedObject.transform.position,
                        ray.GetPoint(initialDistance), ref _velocity, mouseSpeed);
                    yield return null;
                }
            }
            iDrag?.OnDragEnd();
        }
    }
}