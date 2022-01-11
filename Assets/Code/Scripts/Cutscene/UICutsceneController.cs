using System.Collections;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Cutscene
{
    public class UICutsceneController : MonoBehaviour
    {
        public Animator animator;
        public TextMeshProUGUI bigText;

        public GameObject dialogueCanvas;
        public GameObject canvas;
        
        private void Start()
        {
            var dialogueRunner = FindObjectOfType<DialogueRunner>();
            dialogueRunner.AddCommandHandler("playTransition", PlayTransition);
            dialogueRunner.AddCommandHandler("resetTransition", ResetTransition);
            dialogueRunner.AddCommandHandler("showBigText", ShowBigText);
            dialogueRunner.AddCommandHandler("toggleDialogueUI", ToggleDialogueUI);
        }

        public void PlayTransition(string[] parameters, System.Action onComplete)
        {
            var animationState = parameters[0];

            StartCoroutine(PlayAnimation(animationState, onComplete));
        }

        public void ShowBigText(string[] parameters, System.Action onComplete)
        {
            var text = parameters[0];

            bigText.text = text;
            StartCoroutine(PlayAnimation("ShowBigText", onComplete));
        }

        public void ToggleDialogueUI(string[] parameters)
        {
            var isTrue = parameters.Length > 0 || parameters[0] != "false";
            
            dialogueCanvas.SetActive(isTrue);
        }

        public IEnumerator PlayAnimation(string animationState, System.Action onComplete)
        {
            canvas.SetActive(true);
            animator.enabled = true;
            animator.Play(animationState);

            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName(animationState));
            yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(animationState));

            onComplete();
        }

        public void ResetTransition(string[] parameters)
        {
            animator.enabled = false;
            canvas.SetActive(false);
        }
    }
}