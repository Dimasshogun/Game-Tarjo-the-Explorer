using UnityEngine;

namespace Code.Scripts.Character
{
    public class NpcDetector : MonoBehaviour
    {
        private NpcInteract _interact;

        private void Start()
        {
            _interact = GetComponent<NpcInteract>();
            _interact.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                _interact.SetTargetNpc(other.gameObject);
                _interact.enabled = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                _interact.enabled = false;
            }
        }
    }
}
