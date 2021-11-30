using System;
using System.Collections.Generic;

namespace Code.Scripts.Quest
{
    [Serializable]
    public struct QuestStage
    {
        public string title;
        public string instruction;
        public string relatedNpc;
        public string[] requirements;
        public QuestStatus status;
    }

    [Serializable]
    public enum QuestStatus
    {
        Inactive,
        TaskPending,
        TaskDone,
        Success,
        Fail
    }

    public enum QuestStartingType
    {
        Instant,
        Triggered
    }
    
    [Serializable]
    public class QuestData
    {
        public string code;
        public string name;
        public QuestStartingType startingType;
        public QuestStatus status = QuestStatus.Inactive;
        public int currentStage = 0;
        public List<QuestStage> stages;
    }
}