using UnityEngine;

namespace Code.Scripts
{
    public class MinigameManager : MonoBehaviour
    {
        private static MinigameManager _instance;

        public static MinigameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MinigameManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("Minigame Manager Not Found!");
                    }
                }

                return _instance;
            }
        }

        public bool completed;
        
        public void EndMinigame()
        {
            completed = true;
        }
    }
}