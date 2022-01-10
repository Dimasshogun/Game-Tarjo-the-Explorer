using System.Linq;
using UnityEngine;

namespace Code.Scripts.Character.NPC
{
    public class NpcDialogue : MonoBehaviour
    {
        public string characterName = "";
        public string entryNode = "";
        public Transform talkSpot;
        public GameObject questMarker;
        
        [SerializeField] private YarnProgram randomDialogue;


        private void Start()
        {
            if (questMarker == null)
            {
                questMarker = GetComponent<NpcQuest>().marker;
            }
            if (randomDialogue != null)
            {
                var nodes = randomDialogue.GetProgram().Nodes.Keys.ToArray();
                var selectRandom = Random.Range(0, nodes.Length);
                entryNode = nodes[selectRandom];
            }
        }
    }
}