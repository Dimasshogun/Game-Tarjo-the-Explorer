using System.IO;
using Code.Scripts.Quest;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Character
{
    public class NpcController : MonoBehaviour
    {
        public string characterName = "";
        public string entryNode = "";
        
        public YarnProgram dialogueScript;
        
        public string questFile = "";
        
        public QuestData npcQuest = new QuestData();

        private void Start () {
            if (dialogueScript != null) {
                DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
                dialogueRunner.Add(dialogueScript);                
            }

            if (questFile != "")
            {

                var questJson = Resources.Load<TextAsset>($"Quest/{questFile}");
                npcQuest = JsonUtility.FromJson<QuestData>(questJson.ToString());
                FindObjectOfType<QuestManager>().quests.Add(npcQuest);
            }
            
        }
    }
}