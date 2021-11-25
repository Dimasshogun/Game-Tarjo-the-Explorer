using System;
using Code.Scripts.Quest;
using TMPro;
using UnityEngine;

namespace Code.Scripts.NPC
{
    public class NpcQuest : MonoBehaviour
    {
        [SerializeField] private GameObject marker;

        private void Start()
        {
            QuestManager.Instance.UpdateNpc += UpdateMarker;
        }

        private void UpdateMarker(QuestStage stage)
        {
            if (stage.relatedNpc != gameObject.name)
            {
                return;
            }

            Debug.Log("update marker");
            switch (stage.status)
            {
                case QuestStatus.Inactive:
                    marker.SetActive(true);
                    marker.GetComponentInChildren<TextMeshProUGUI>().text = "!";
                    marker.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                    break;
                case QuestStatus.TaskPending:
                    marker.SetActive(true);
                    marker.GetComponentInChildren<TextMeshProUGUI>().text = "!";
                    marker.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    break;
                case QuestStatus.TaskDone:
                    marker.SetActive(true);
                    marker.GetComponentInChildren<TextMeshProUGUI>().text = "?";
                    marker.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                    break;
                case QuestStatus.Success:
                case QuestStatus.Fail:
                    marker.SetActive(false);
                    QuestManager.Instance.UpdateNpc -= UpdateMarker;
                    break;
                default:
                    break;
            }
        }
    }
}