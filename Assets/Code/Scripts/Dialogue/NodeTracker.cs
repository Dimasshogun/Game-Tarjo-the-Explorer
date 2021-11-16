using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts
{
    public class NodeTracker : MonoBehaviour
    {
        [SerializeField] private DialogueRunner dialogueRunner;
        private HashSet<string> _visitedNode = new HashSet<string>();

        private void Start()
        {
            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }
            
            dialogueRunner.AddFunction("visited", 1, delegate (Yarn.Value[] parameters)
            {
                var nodeName = parameters[0];
                return _visitedNode.Contains(nodeName.AsString);
            });
        }

        public void SetVisited(string nodeName)
        {
            _visitedNode.Add(nodeName);
        }
    }
}