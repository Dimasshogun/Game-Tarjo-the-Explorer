using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Quest
{
    public class QuestNotification : MonoBehaviour
    {
        private static QuestNotification _instance;

        public static QuestNotification Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<QuestNotification>();
                    if (_instance == null)
                    {
                        Debug.LogError("Quest Notification Not Found!");
                    }
                }
                return _instance;
            }
        }
        
        [SerializeField] private TextMeshProUGUI questTitle;
        [SerializeField] private TextMeshProUGUI questInstruction;
        [SerializeField] private GameObject questContainer;

        private void Awake()
        {
            if (questTitle == null)
            {
                questTitle = GameObject.Find("QuestTitle").GetComponent<TextMeshProUGUI>();
            }
            if (questInstruction == null)
            {
                questInstruction = GameObject.Find("QuestInstruction").GetComponent<TextMeshProUGUI>();
            }
            if (questContainer == null)
            {
                questContainer = GameObject.Find("QuestContainer");
            }
        }

        private void Start()
        {
            questContainer.SetActive(false);
        }

        public void ShowNotification(QuestStage questStage)
        {
            StartCoroutine(RunNotification(questStage));
        }
        
        private IEnumerator RunNotification(QuestStage questStage)
        {
            questTitle.text = questStage.title;
            questInstruction.text = questStage.instruction;
            
            questContainer.SetActive(true);

            yield return new WaitForSeconds(2.0f);
            
            questContainer.SetActive(false);
        }
    }
}