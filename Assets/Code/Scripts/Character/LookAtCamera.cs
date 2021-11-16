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
            _modelTransform = GetComponentInChildren<SpriteRenderer>().transform;
        }

        private void Update()
        {
            TurnToCamera();
        }

        private void TurnToCamera()
        {
            var cameraPos = _mainCameraTransform.transform.position;

            cameraPos.y = transform.position.y;
        
            _modelTransform.LookAt(cameraPos);
        }
    }
}