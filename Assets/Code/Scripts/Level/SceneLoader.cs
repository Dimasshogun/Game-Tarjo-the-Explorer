using UnityEngine;

namespace Code.Scripts.Level
{
    public class SceneLoader : MonoBehaviour
    {
        public void StartLoadLevel(string level)
        {
            LevelManager.Instance.StartLoadLevel(level);
        }
        public void StartChangeScene(string scene)
        {
            LevelManager.Instance.StartChangeScene(scene);
        }
        
        public void ExitGame()
        {
            LevelManager.Instance.ExitGame();
        }
    }
}