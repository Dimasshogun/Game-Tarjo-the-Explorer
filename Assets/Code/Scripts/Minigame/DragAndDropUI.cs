using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.Minigame
{
    public class DragAndDropUI : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 _initialPosition;
        private RectTransform _objectRectTransform;
        private Vector3 _targetDropPosition;
        
        [SerializeField] private float speed;
        [SerializeField] private int answerValue;
        
        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            _objectRectTransform = transform as RectTransform;
            _initialPosition = _objectRectTransform.position;
            _targetDropPosition = GameObject.Find("DropTarget").GetComponent<RectTransform>().position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_objectRectTransform, eventData.position, eventData.pressEventCamera, out var mousePos))
            {
                _objectRectTransform.position =
                    Vector3.SmoothDamp(_objectRectTransform.position, mousePos, ref _velocity, speed);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var distance = Vector3.Distance(_objectRectTransform.position, _targetDropPosition);
            if (distance < 50)
            {
                if (MinigameManager.Instance.CheckAnswer(answerValue))
                {
                    _objectRectTransform.position = _targetDropPosition;
                    MinigameManager.Instance.PlayCorrect();
                }
                else
                {
                    _objectRectTransform.position = _initialPosition;
                    MinigameManager.Instance.PlayIncorrect();
                }
            }
            else
            {
                _objectRectTransform.position = _initialPosition;
            }
        }
    }
}