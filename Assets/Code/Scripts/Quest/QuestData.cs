using System;
using System.Collections.Generic;

namespace Code.Scripts.Quest
{
    [Serializable]
    public struct QuestStage
    {
        public string title;
        public string instruction;
        public QuestStatus status;
    }

    [Serializable]
    public enum QuestStatus
    {
        Pending,
        Active,
        Success,
        Fail
    }
    
    [Serializable]
    public class QuestData
    {
        public string code;
        public string name;
        public QuestStatus status = QuestStatus.Pending;
        public int currentStage = 0;
        public List<QuestStage> stages;

        public string StartQuest()
        {
            status = QuestStatus.Active;
            return name;
        }

        public QuestStage AdvanceStage(int stageNumber)
        {
            currentStage = stageNumber;
            status = stages[currentStage].status;
            return stages[currentStage];
        }
    }
}