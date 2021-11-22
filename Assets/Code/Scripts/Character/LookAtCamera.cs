using System;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _modelTransform;
        private Transform _mainCameraTransform;

        private void Start()
        {
            _mainCameraTransform = Camera.main.transform;
            _modelTransform = GetComponentsInChildren<Transform>()[1];
        }

        private void Update()
        {
            TurnToCamera();
        }

        private void TurnToCamera()
        {
            var cameraPos = _mainCameraTransform.transform.position;

            var currentPos = transform.position;
            cameraPos.y = currentPos.y;
        
            _modelTransform.LookAt(2 * currentPos - cameraPos);
        }
    }
}