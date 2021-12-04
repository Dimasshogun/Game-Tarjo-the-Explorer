using UnityEngine;

namespace Code.Scripts.Minigame
{
    public class MinigameDragUI : MonoBehaviour, IDrag
    {
        private Vector3 _initialPosition;
        private RectTransform _objectRectTransform;

        public string answerValue;
        
        public MinigameDropTarget target;
        

        private void Awake()
        {
            _objectRectTransform = transform as RectTransform;
            _initialPosition = _objectRectTransform.position;
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
            if (target == null)
            {
                _objectRectTransform.position = _initialPosition;
                return;
            }
            if (target.correctAnswer == answerValue)
            {
                target.isCorrect = true;
                _objectRectTransform.position = target.transform.position;
                MinigameManager.Instance.PlayCorrect();
                return;
            }
            _objectRectTransform.position = _initialPosition;
            MinigameManager.Instance.PlayIncorrect();
            target = null;
        }
    }
}