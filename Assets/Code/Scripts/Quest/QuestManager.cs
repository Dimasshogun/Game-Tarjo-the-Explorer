using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private DialogueRunner dialogueRunner;
        public List<QuestData> quests = new List<QuestData>();

        private void Start()
        {
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
            var startedQuest = quests.Find(quest => quest.code == questCode).StartQuest();
        }

        [YarnCommand("advanceQuestStage")]
        public void AdvanceQuestStage(string[] parameters)
        {
            var questCode = parameters[0];
            var stage = Int32.Parse(parameters[1]);
            var questStage = quests.Find(quest => quest.code == questCode).AdvanceStage(stage);
        }
    }
}