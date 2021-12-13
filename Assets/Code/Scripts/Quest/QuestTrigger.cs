using System;
using System.Collections;
using Cinemachine;
using Code.Scripts.Character;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Quest
{
    public class QuestTrigger : MonoBehaviour
    {
        public string questCode;
        public string dialogueNode;
        
        private bool _triggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !_triggered)
            {
                _triggered = true;
                StartCoroutine(StartQuestTrigger());
            }
        }

        private IEnumerator StartQuestTrigger()
        {
            var questStage = QuestManager.Instance.GetQuestStage(questCode);
            var npcCamera = GameObject.Find(questStage.relatedNpc).GetComponentInChildren<CameraInstance>();
            var playerMovement = GameObject.FindWithTag("Player").GetComponent<Movement>();
            var dialogueRunner = FindObjectOfType<DialogueRunner>();

            playerMovement.enabled = false;
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(CameraManager.Instance.SwitchVirtualCam(npcCamera));

            if (dialogueNode != "")
            {
                dialogueRunner.StartDialogue(dialogueNode);
                yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);
            }
            else
            {
                yield return new WaitForSeconds(2.0f);
                QuestManager.Instance.StartQuest(new []{questCode});
            }
            
            
            yield return new WaitForSeconds(2.0f);
            CameraManager.Instance.SwitchToPlayerCam();
            
            yield return new WaitForSeconds(1.0f);
            gameObject.SetActive(false);
            playerMovement.enabled = true;
            QuestManager.Instance.RunNpcUpdate();
        }
    }
}