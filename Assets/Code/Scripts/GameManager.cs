using Code.Scripts.Quest;
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

        public LevelProgress levelProgress;

        private GameObject _player;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            
            levelProgress = SaveDataManager.LoadLevelProgress();
            if (levelProgress.playerPos != Vector3.zero)
            {
                _player.transform.position = levelProgress.playerPos;
            }

            if (levelProgress.quests.Count != 0)
            {
                return;
            }

            var quests = QuestManager.Instance.quests;
            foreach (var quest in quests)
            {
                levelProgress.quests.Add(new QuestProgress {questCode = quest.QuestCode, currentStage = 0});
            }
        }

        public void SaveProgress()
        {
            levelProgress.playerPos = _player.transform.position;
            SaveDataManager.SaveLevelProgress(levelProgress);
        }
    }
}