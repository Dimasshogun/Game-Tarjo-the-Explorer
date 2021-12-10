using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        public void StartChangeScene(string scene)
        {
            // StartCoroutine(LevelManager.Instance.ChangeScene(scene));
            SceneManager.LoadScene(scene);
        }
    }
}