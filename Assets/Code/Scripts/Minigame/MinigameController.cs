using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

namespace Code.Scripts.Minigame
{
    public class MinigameController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueUI;
        
        private void Start()
        {
            FindObjectOfType<DialogueRunner>().AddCommandHandler("startMinigame", StartMinigame);
            if (dialogueUI == null)
            {
                dialogueUI = GameObject.Find("DialogueUI");
            }
        }

        public void StartMinigame(string[] parameters, System.Action onComplete)
        {
            var minigameScene = parameters[0];
            
            StartCoroutine(PlayMinigame(minigameScene, onComplete));
        }
        
        public IEnumerator PlayMinigame(string minigameScene, System.Action onComplete)
        {
            dialogueUI.SetActive(false);
            
            SceneManager.LoadSceneAsync(minigameScene, LoadSceneMode.Additive);
            
            yield return new WaitUntil(() => SceneManager.GetSceneByName(minigameScene).isLoaded);
            
            yield return new WaitUntil(() => MinigameManager.Instance.completed);

            SceneManager.UnloadSceneAsync(minigameScene);

            dialogueUI.SetActive(true);
            onComplete();
        }
    }
}