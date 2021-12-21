using System;
using System.Collections.Generic;
using Code.Scripts.NPC;
using Code.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace Code.Scripts.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager _instance;

        public static QuestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<QuestManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("Quest Manager Not Found!");
                    }
                }
                return _instance;
            }
        }
        
        [SerializeField] private DialogueRunner dialogueRunner;
        public List<QuestData> quests = new List<QuestData>();

        private GameManager _gameManager;
        private Queue<QuestStage> _updateNpcQueue = new Queue<QuestStage>();

        public UnityAction<QuestStage> UpdateNpc = delegate {  };

        private void Start()
        {
            _gameManager = GameManager.Instance;
            foreach (var quest in quests)
            {
                _gameManager.levelProgress.quests.Add(new QuestProgress(){questCode = quest.QuestCode, currentStage = 0});
                if (quest.StartingType == QuestStartingType.Instant)
                {
                    StartQuest(new[] {quest.QuestCode});
                }
            }
            
            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }
            
            dialogueRunner.AddCommandHandler("startQuest", StartQuest);
            dialogueRunner.AddCommandHandler("advanceQuestStage", AdvanceQuestStage);
            
            dialogueRunner.AddFunction("questStage", 2, delegate (Yarn.Value[] parameters)
            {
                var questCode = parameters[0].AsString;
                var stageIndex =  (int)parameters[1].AsNumber;
                var quest = _gameManager.levelProgress.quests.Find(quest => quest.questCode == questCode);
                return quest.currentStage == stageIndex;
            });
        }
        
        public void StartQuest(string[] parameters)
        {
            var questCode = parameters[0];
            
            var quest = quests.Find(quest => quest.QuestCode == questCode);
            var currentQuestStage = _gameManager.levelProgress.quests.Find(q => q.questCode == questCode).currentStage;
            _updateNpcQueue.Enqueue(quest.Stages[currentQuestStage]);
            
            QuestNotification.Instance.ShowNotification(quest.Stages[currentQuestStage]);
        }

        public void AdvanceQuestStage(string[] parameters)
        {
            var questCode = parameters[0];
            var stage = int.Parse(parameters[1]);
            
            var quest = quests.Find(quest => quest.QuestCode == questCode);
            var updatedQuestStage = _gameManager.UpdateQuestStage(questCode, stage);
            _updateNpcQueue.Enqueue(quest.Stages[updatedQuestStage]);
            
            QuestNotification.Instance.ShowNotification(quest.Stages[updatedQuestStage]);
        }

        public QuestStage GetQuestStage(string questCode)
        {
            var quest = quests.Find(quest => quest.QuestCode == questCode);
            var currentQuestStage = _gameManager.GetQuestStage(questCode);
            return quest.Stages[currentQuestStage];
        }
        
        public string GetQuestNpc(string questCode)
        {
            var quest = quests.Find(quest => quest.QuestCode == questCode);
            var currentQuestStage = _gameManager.GetQuestStage(questCode);
            return quest.Stages[currentQuestStage].relatedNpc;
        }

        public void RunNpcUpdate()
        {
            while (_updateNpcQueue.Count > 0)
            {
                var updatedStage = _updateNpcQueue.Dequeue();
                UpdateNpc(updatedStage);
            }
        }
    }
}