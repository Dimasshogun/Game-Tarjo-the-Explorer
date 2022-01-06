using System;
using System.Collections;
using Code.Scripts.Control;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

namespace Code.Scripts.Cutscene
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private string scene;
        [SerializeField] private string dialogueNode;
        [SerializeField] private MovementInputHandler playerMovement;

        private bool _triggered;

        private void Start()
        {
            if (playerMovement == null)
            {
                playerMovement = GameObject.FindWithTag("Player").GetComponent<MovementInputHandler>();
            }
        }

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
            playerMovement.enabled = false;
            if (scene != "" && !SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
            
            yield return new WaitUntil(() => SceneManager.GetSceneByName(scene).isLoaded);

            var dialogueRunner = FindObjectOfType<DialogueRunner>();

            if (dialogueNode != "" && !dialogueRunner.IsDialogueRunning)
            {
                yield return new WaitUntil(() => dialogueRunner.NodeExists(dialogueNode));
                dialogueRunner.StartDialogue(dialogueNode);
            }
            
            yield return new WaitUntil(() => !dialogueRunner.IsDialogueRunning);

            SceneManager.UnloadSceneAsync(scene);
            playerMovement.enabled = true;
        }
    }
}