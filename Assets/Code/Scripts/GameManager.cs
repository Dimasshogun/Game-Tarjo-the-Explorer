using Code.Scripts.SaveSystem;
using UnityEngine;

namespace Code.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    Debug.LogError("Game Manager Not Found!");
                }

                return _instance;
            }
        }

        public LevelProgress levelProgress = new LevelProgress();

        public int UpdateQuestStage(string questCode, int stage)
        {
            var quest = levelProgress.quests.Find(q => q.questCode == questCode);
            quest.currentStage = stage;

            Debug.Log(levelProgress.quests.Find(q => q.questCode == questCode).currentStage);

            return quest.currentStage;
        }

        public int GetQuestStage(string questCode)
        {
            return levelProgress.quests.Find(q => q.questCode == questCode).currentStage;
        }
        
    }
}