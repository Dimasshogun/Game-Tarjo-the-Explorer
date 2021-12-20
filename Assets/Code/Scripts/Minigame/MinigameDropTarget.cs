using UnityEngine;

namespace Code.Scripts.Minigame
{
    public class MinigameDropTarget : MonoBehaviour
    {
        public string correctAnswer;
        public bool isCorrect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MinigameDrag>(out var dragObject))
            {
                dragObject.target = this;
            }
            
            if (other.TryGetComponent<MinigameDragUI>(out var dragObjectUI))
            {
                dragObjectUI.target = this;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<MinigameDrag>(out var dragObject))
            {
                dragObject.target = null;
            }
            
            if (other.TryGetComponent<MinigameDragUI>(out var dragObjectUI))
            {
                dragObjectUI.target = null;
            }
        }
    }
}