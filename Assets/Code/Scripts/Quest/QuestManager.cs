using System;
using System.Collections.Generic;
using Code.Scripts.NPC;
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
        [SerializeField] private string[] questFiles;
        public List<QuestData> quests = new List<QuestData>();
        
        public UnityAction<QuestStage> UpdateNpc = delegate {  };

        private void Start()
        {
            foreach (var questFile in questFiles)
            {
                var questJson = Resources.Load<TextAsset>($"Quest/{questFile}");
                if (questJson != null)
                {
                    var npcQuest = JsonUtility.FromJson<QuestData>(questJson.ToString());
                    quests.Add(npcQuest);
                    
                    if (npcQuest.startingType == QuestStartingType.Instant)
                    {
                        StartQuest(new[] {npcQuest.code});
                    }
                }
            }
            
            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }
            
            dialogueRunner.AddFunction("questStage", 2, delegate (Yarn.Value[] parameters)
            {
                var questCode = parameters[0].AsString;
                var stageIndex =  (int)parameters[1].AsNumber;
                var quest = quests.Find(quest => quest.code == questCode);
                return quest.currentStage == stageIndex;
            });
        }
        
        [YarnCommand("startQuest")]
        public void StartQuest(string[] parameters)
        {
            var questCode = parameters[0];
            
            var quest = quests.Find(quest => quest.code == questCode);
            quest.status = quest.stages[quest.currentStage].status;
            UpdateNpc(quest.stages[quest.currentStage]);
            
            // should change with quest notification
            Debug.Log($"Started {quest.name}");
        }

        [YarnCommand("advanceQuestStage")]
        public void AdvanceQuestStage(string[] parameters)
        {
            var questCode = parameters[0];
            var stage = int.Parse(parameters[1]);
            
            var quest = quests.Find(quest => quest.code == questCode);
            quest.currentStage = stage;
            quest.status = quest.stages[quest.currentStage].status;
            UpdateNpc(quest.stages[quest.currentStage]);
            
            Debug.Log($"{quest.name}: {quest.stages[quest.currentStage]}");
        }

        public QuestStage GetQuestStage(string questCode)
        {
            var quest = quests.Find(quest => quest.code == questCode);
            return quest.stages[quest.currentStage];
        }
        
        public string GetQuestNpc(string questCode)
        {
            var quest = quests.Find(quest => quest.code == questCode);
            return quest.stages[quest.currentStage].relatedNpc;
        }
    }
}