using Code.Scripts.Quest;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Character.NPC
{
    public class NpcQuest : MonoBehaviour
    {
        public GameObject marker;
        
        private TextMeshProUGUI markerText;

        private void Start()
        {
            markerText = marker.GetComponentInChildren<TextMeshProUGUI>();
            QuestManager.Instance.UpdateNpc += UpdateMarker;
        }

        private void UpdateMarker(QuestStage stage)
        {
            if (stage.relatedNpc != gameObject.name)
            {
                return;
            }

            switch (stage.status)
            {
                case QuestStatus.Inactive:
                    marker.SetActive(true);
                    markerText.text = "!";
                    markerText.color = Color.yellow;
                    break;
                case QuestStatus.TaskPending:
                    marker.SetActive(true);
                    markerText.text = "!";
                    markerText.color = Color.white;
                    break;
                case QuestStatus.TaskDone:
                    marker.SetActive(true);
                    markerText.text = "?";
                    markerText.color = Color.yellow;
                    break;
                case QuestStatus.Success:
                case QuestStatus.Fail:
                    marker.SetActive(false);
                    markerText.text = "";
                    QuestManager.Instance.UpdateNpc -= UpdateMarker;
                    break;
                default:
                    marker.SetActive(false);
                    markerText.text = "";
                    break;
            }
        }
    }
}