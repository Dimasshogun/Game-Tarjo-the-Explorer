using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

namespace Code.Scripts.Cutscene
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private string scene;
        [SerializeField] private string dialogueNode;

        private bool _triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_triggered)
            {
                _triggered = true;
                StartCoroutine(PlayCutscene());
            }
        }

        private IEnumerator PlayCutscene()
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            
            yield return new WaitUntil(() => SceneManager.GetSceneByName(scene).isLoaded);

            var dialogueRunner = FindObjectOfType<DialogueRunner>();

            if (dialogueNode != "" && !dialogueRunner.IsDialogueRunning)
            {
                yield return new WaitUntil(() => dialogueRunner.NodeExists(dialogueNode));
                dialogueRunner.StartDialogue(dialogueNode);
            }
            
            yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);
            Debug.Log("dialogue end");

            SceneManager.UnloadSceneAsync(scene);
        }
    }
}