using System;
using System.Collections.Generic;
using UnityEngine;

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
    
    [CreateAssetMenu(fileName = "QuestData", menuName = "Project/Quest Data", order = 0)]
    public class QuestData: ScriptableObject
    {
        [SerializeField] private string questCode;
        [SerializeField] private string questName;
        [SerializeField] private QuestStartingType startingType;
        [SerializeField] private List<QuestStage> stages;

        public string QuestCode => questCode;
        public QuestStartingType StartingType => startingType;
        public List<QuestStage> Stages => stages;
    }
}