using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.Minigame
{
    public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _objectRectTransform;

        [SerializeField] private float speed;
        
        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            _objectRectTransform = transform as RectTransform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            TryGetComponent<IDrag>(out var iDrag);
            iDrag?.OnDragStart();
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
            TryGetComponent<IDrag>(out var iDrag);
            iDrag.OnDragEnd();
        }
    }
}