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
        
        public YarnProgram dialogueScript;

        private void Start ()
        {
            if (dialogueScript == null)
            {
                return;
            }

            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            if ( !dialogueRunner.NodeExists(entryNode))
            {
                dialogueRunner.Add(dialogueScript);                
            }
        }
    }
}