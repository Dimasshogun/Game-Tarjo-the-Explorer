using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.Minigame
{
    public class DragAndDropUI : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 _initialPosition;
        private RectTransform _objectRectTransform;

        [SerializeField] private RectTransform _targetDrop;
        [SerializeField] private float speed;
        [SerializeField] private int answerValue;
        
        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            _objectRectTransform = transform as RectTransform;
            _initialPosition = _objectRectTransform.position;
            // Set _targetDropPosition dengan objek bernama DropTarget kalau belum diset dari Inspector
            if (_targetDrop == null)
            {
                _targetDrop = GameObject.Find("DropTarget").GetComponent<RectTransform>();
            }
        }

        //Saat pemain drag objek
        public void OnDrag(PointerEventData eventData)
        {
            // Ubah koordinat kursor di layar ke posisi UI
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_objectRectTransform, eventData.position, eventData.pressEventCamera, out var mousePos))
            {
                // Pindahkan objek sesuai posisi kursor
                _objectRectTransform.position =
                    Vector3.SmoothDamp(_objectRectTransform.position, mousePos, ref _velocity, speed);
            }
        }

        // Saat pemain drop objek
        public void OnEndDrag(PointerEventData eventData)
        {
            // Hitung jarak antara objek dan _targetDropPosition
            var distance = Vector3.Distance(_objectRectTransform.position, _targetDrop.position);
            // jika jarak kurang dari 50 unit
            if (distance < 50)
            {
                // cek apakah jawaban dalam objek (answerValue) sesuai dengan kunci jawaban (correctAnswer)
                if (MinigameManager.Instance.CheckAnswer(answerValue))
                {
                    // Jika iya, letakkan objek didalam dropzone (_targetDropPosition)
                    _objectRectTransform.position = _targetDrop.position;
                    // Jalankan fungsi PlayCorrect()
                    MinigameManager.Instance.PlayCorrect();
                }
                else
                {
                    // Jika tidak, kembalikan objek ke tempat semula (_initialPosition)
                    _objectRectTransform.position = _initialPosition;
                    // Jalankan fungsi PlayIncorrect()
                    MinigameManager.Instance.PlayIncorrect();
                }
            }
            else
            {
                // jika jarak lebih dari 50 unit, kembalikan objek ke tempat semula (_initialPosition)
                _objectRectTransform.position = _initialPosition;
            }
        }
    }
}