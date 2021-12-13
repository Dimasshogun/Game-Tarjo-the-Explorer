using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts
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