using System;
using System.Collections.Generic;
using UnityEngine;

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
        public Vector3 playerPos;
        public List<QuestProgress> quests;

        public DateTime lastSave;

        public LevelProgress(SavedLevelProgress savedLevelProgress)
        {
            playerPos = new Vector3(
                savedLevelProgress.playerPosX, savedLevelProgress.playerPosY, savedLevelProgress.playerPosZ);

            quests = savedLevelProgress.quests;
            
            lastSave = DateTime.FromFileTime(savedLevelProgress.lastSave);
        }
        
        public LevelProgress()
        {
            playerPos = Vector3.zero;
            quests = new List<QuestProgress>();
            lastSave = DateTime.Now;
        }

        public int UpdateQuestStage(string questCode, int stage)
        {
            var quest = quests.Find(q => q.questCode == questCode);
            quest.currentStage = stage;

            return quest.currentStage;
        }

        public int GetQuestStage(string questCode)
        {
            return quests.Find(q => q.questCode == questCode).currentStage;
        }
    }
    
    [Serializable]
    public class SavedLevelProgress
    {
        public float playerPosX, playerPosY, playerPosZ;
        public List<QuestProgress> quests;

        public long lastSave;

        public SavedLevelProgress(LevelProgress levelProgress)
        {
            playerPosX = levelProgress.playerPos.x;
            playerPosY = levelProgress.playerPos.y;
            playerPosZ = levelProgress.playerPos.z;

            quests = levelProgress.quests;

            lastSave = levelProgress.lastSave.ToFileTime();
        }
    }
}