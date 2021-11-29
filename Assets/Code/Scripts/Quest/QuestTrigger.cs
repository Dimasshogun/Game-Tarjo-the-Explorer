using System;
using System.Collections;
using Cinemachine;
using Code.Scripts.Character;
using UnityEngine;

namespace Code.Scripts.Quest
{
    public class QuestTrigger : MonoBehaviour
    {
        public string questCode;
        
        private bool _triggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered)
            {
                StartCoroutine(StartQuestTrigger());
            }
        }

        private IEnumerator StartQuestTrigger()
        {
            var questStage = QuestManager.Instance.GetQuestStage(questCode);
            var npcCamera = GameObject.Find(questStage.relatedNpc).GetComponentInChildren<CinemachineVirtualCamera>();
            var playerMovement = GameObject.FindWithTag("Player").GetComponent<Movement>();

            playerMovement.enabled = false;
            yield return new WaitForSeconds(1.0f);
            CameraManager.Instance.SwitchToNpcCam(npcCamera);
            
            yield return new WaitForSeconds(2.0f);
            QuestManager.Instance.StartQuest(new []{questCode});
            
            yield return new WaitForSeconds(2.0f);
            CameraManager.Instance.SwitchToPlayerCam();
            
            yield return new WaitForSeconds(1.0f);
            _triggered = true;
            gameObject.SetActive(false);
            playerMovement.enabled = true;
            QuestManager.Instance.RunNpcUpdate();
        }
    }
}