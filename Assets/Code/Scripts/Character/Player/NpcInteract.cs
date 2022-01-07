using System.Collections;
using Code.Scripts.Character.NPC;
using Code.Scripts.Control;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Yarn.Unity;

namespace Code.Scripts.Character.Player
{
    public class NpcInteract : MonoBehaviour
    {
        private NpcDialogue _targetNpc;
        
        public DialogueRunner dialogueRunner;
        public Button interactButton;
        private bool _onDialogue;
        
        private void Awake() 
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
            interactButton.onClick.AddListener(() => StartCoroutine(StartDialog()));
        }
        
        private void Update()
        {
            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            if (kb.spaceKey.wasPressedThisFrame)
            {
                StartCoroutine(StartDialog());
            }
        }

        private void OnEnable()
        {
            if (_targetNpc != null && dialogueRunner.NodeExists(_targetNpc.entryNode))
            {
                if (interactButton != null)
                {
                    interactButton.gameObject.SetActive(true);
                }
            }
            else
            {
                enabled = false;
            }
        }
        
        private void OnDisable()
        {
            if (interactButton != null)
            {
                interactButton.gameObject.SetActive(false);
            }
        }

        private IEnumerator StartDialog()
        {
            if (!_onDialogue)
            {
                dialogueRunner.StartDialogue(_targetNpc.entryNode);
                ToggleOnDialogue(true);
            }

            yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);
            
            ToggleOnDialogue(false);
        }

        public void SetTargetNpc(GameObject npc)
        {
            _targetNpc = npc.GetComponent<NpcDialogue>();
        }

        public void ToggleOnDialogue(bool onDialogue)
        {
            _onDialogue = onDialogue;
            GetComponent<MovementInputHandler>().enabled = !onDialogue;

            if (onDialogue)
            {
                var targetNpcCam = _targetNpc.gameObject.GetComponentInChildren<CameraInstance>();
                StartCoroutine(CameraManager.Instance.SwitchVirtualCam(targetNpcCam));
            }
            else
            {
                CameraManager.Instance.SwitchToPlayerCam();
            }
        }
    }
}