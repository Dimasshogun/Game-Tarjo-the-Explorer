using System.Linq;
using UnityEngine;

namespace Code.Scripts.Character.NPC
{
    public class NpcDialogue : MonoBehaviour
    {
        public string characterName = "";
        public string entryNode = "";
        [SerializeField] private YarnProgram randomDialogue;

        private void Start()
        {
            if (randomDialogue != null)
            {
                var nodes = randomDialogue.GetProgram().Nodes.Keys.ToArray();
                var selectRandom = Random.Range(0, nodes.Length);
                entryNode = nodes[selectRandom];
            }
        }
    }
}