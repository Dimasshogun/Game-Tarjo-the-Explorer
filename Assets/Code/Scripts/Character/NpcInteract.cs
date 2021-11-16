using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Code.Scripts.Character
{
    public class NpcInteract : MonoBehaviour
    {
        private NpcController _targetNpc;
        
        public DialogueRunner dialogueRunner;
        public Button interactButton;
        private bool _onDialogue;
        
        private void Start() 
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
            interactButton.onClick.AddListener(StartDialog);
        }
        
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartDialog();
            }
        }

        private void OnEnable()
        {
            interactButton.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            interactButton.gameObject.SetActive(false);
        }

        public void StartDialog()
        {
            if (!_onDialogue)
            {
                Cursor.lockState = CursorLockMode.None;
                dialogueRunner.StartDialogue(_targetNpc.entryNode);
                ToggleOnDialogue(true);
            }
        }

        public void SetTargetNpc(GameObject npc)
        {
            _targetNpc = npc.GetComponent<NpcController>();
        }

        public void ToggleOnDialogue(bool onDialogue)
        {
            _onDialogue = onDialogue;
            // temporary usage
            GetComponent<Movement>().enabled = !onDialogue;

            if (onDialogue)
            {
                Cursor.lockState = CursorLockMode.None;
                var targetNpcCam = _targetNpc.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
                Debug.Log(targetNpcCam);
                CameraManager.Instance.SwitchToNpcCam(targetNpcCam);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CameraManager.Instance.SwitchToPlayerCam();
            }
        }
    }
}