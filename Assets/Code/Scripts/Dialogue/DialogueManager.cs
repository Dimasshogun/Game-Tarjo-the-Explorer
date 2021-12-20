using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private YarnProgram[] dialogues;

        private void Start()
        {
            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            foreach (var dialogue in dialogues)
            {
                dialogueRunner.Add(dialogue);
            }
        }
    }
}