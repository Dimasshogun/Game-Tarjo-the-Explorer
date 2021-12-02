using System.IO;
using Code.Scripts.Quest;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.NPC
{
    public class NpcDialogue : MonoBehaviour
    {
        public string characterName = "";
        public string entryNode = "";
        public bool instantExec = false;
        
        public YarnProgram dialogueScript;

        private void Start ()
        {
            if (dialogueScript == null)
            {
                return;
            }

            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            dialogueRunner.Add(dialogueScript);
            if (instantExec)
            {
                dialogueRunner.StartDialogue(entryNode);
            }
        }
    }
}