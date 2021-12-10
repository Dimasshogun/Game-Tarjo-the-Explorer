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
        
        private void Awake()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public void OnDragStart()
        {
            _rb.isKinematic = false;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            if (target == null)
            {
                return;
            }

            target.isCorrect = false;
            target = null;
        }

        public void OnDrag()
        {
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