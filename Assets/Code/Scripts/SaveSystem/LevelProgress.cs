using System;
using System.Collections.Generic;

namespace Code.Scripts.SaveSystem
{
    [Serializable]
    public class QuestProgress
    {
        public string questCode;
        public int currentStage;
    }
    
    [Serializable]
    public class LevelProgress
    {
        public List<QuestProgress> quests = new List<QuestProgress>();
    }
}