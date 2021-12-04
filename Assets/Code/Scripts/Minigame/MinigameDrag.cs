using System;
using UnityEngine;

namespace Code.Scripts.Minigame
{
    public class MinigameDrag : MonoBehaviour, IDrag
    {
        public string answerValue;
        public MinigameDropTarget target;

        private Vector3 _initialPosition;
        
        private void Awake()
        {
            _initialPosition = transform.position;
        }

        public void OnDragStart()
        {
            if (target == null)
            {
                return;
            }

            target.isCorrect = false;
            target = null;
        }

        public void OnDragEnd()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (target == null)
            {
                transform.position = _initialPosition;
                return;
            }
            
            if (target.correctAnswer == answerValue)
            {
                target.isCorrect = true;
                transform.position = target.transform.position;
                MinigameManager.Instance.PlayCorrect();
                return;
            }
            transform.position = _initialPosition;
            MinigameManager.Instance.PlayIncorrect();
            target = null;
        }
    }
}